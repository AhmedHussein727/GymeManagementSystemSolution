using AutoMapper;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.SessionViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper _mapper { get; }

        public SessionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = UnitOfWork.SessionRepository.GetAllSessionsWithTrainersAndCategory().ToList();
            if (!Sessions.Any()) return [];

            var MappedSessions = _mapper.Map<IEnumerable<Sessions>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - UnitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);

            }
            return MappedSessions;
        }

        public SessionViewModel? GetSessionById(int SessionId)
        {
            var Session=UnitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(SessionId);
            if (Session==null) return null;

            var MappedSession = _mapper.Map<Sessions, SessionViewModel>(Session);
            MappedSession.AvailableSlots=MappedSession.Capacity-UnitOfWork.SessionRepository.GetCountOfBookedSlots(MappedSession.Id);
            return MappedSession;

        }

        public bool CteateSession(CreateSessionViewModel CreatedSession)
        {
            try
            {
                if (!IsTrainerExist(CreatedSession.TrainerId)) return false;
                if (!IsCategoryExist(CreatedSession.CategoryId)) return false;
                if (!IsDateTimeValid(CreatedSession.StartDate, CreatedSession.EndDate)) return false;
                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity <= 0) return false;
                var sessionEntity = _mapper.Map<Sessions>(CreatedSession);
                if (sessionEntity == null) return false;
                UnitOfWork.GetRepository<Sessions>().Add(sessionEntity);
                return UnitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failed :{ex}");

                return false;
            }
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var session=UnitOfWork.GetRepository<Sessions>().GetById(SessionId);
            if(!IsSessionAvailpleForUpdating(session !)) return null;
            return _mapper.Map<UpdateSessionViewModel> (session);


        }

        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int SessionId)
        {
            try
            {
                var session=UnitOfWork.SessionRepository.GetById(SessionId);
                if(!IsSessionAvailpleForUpdating(session) ) return false;
                if(!IsTrainerExist(UpdatedSession.TrainerId)) return false;
                if(!IsDateTimeValid(UpdatedSession.StartDate,UpdatedSession.EndDate)) return false;
                session.UpdatedAt = DateTime.Now;
                _mapper.Map(UpdatedSession,session);
                UnitOfWork.GetRepository<Sessions>().Update(session);
                return UnitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update session Failed : {ex}");
                return false;
            }
        }

        public bool RemoveSession(int SessionId)
        {
            try
            {
                var session = UnitOfWork.SessionRepository.GetById(SessionId);
                if (!IsSessionAvailpleForRemovung(session!)) return false;
                UnitOfWork.GetRepository<Sessions>().Delete(session);
                return UnitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("removing failed: {ex}");
                return false;
            }


        }

        public IEnumerable<TrainerSelectViewModel> GetTrainersForDropDowen()
        {
            var trainers=UnitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoriesForDropDowen()
        {
            var categories=UnitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
        }



        #region Helper Methods
        private bool IsSessionAvailpleForRemovung(Sessions session)
        {
            var HasActiveBooking = UnitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if ((DateTime.Now>session.StartDate && DateTime.Now > session.EndDate&& ! HasActiveBooking))
                return true;
            else
                return false; 
        }

        private bool IsSessionAvailpleForUpdating(Sessions session)
        { 
            return   UnitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) == 0 && session.StartDate > DateTime.Now;
        }
        private bool IsTrainerExist(int TrainerId) => UnitOfWork.GetRepository<Trainer>().GetById(TrainerId) != null;
        private bool IsCategoryExist(int CategoryId) => UnitOfWork.GetRepository<Category>().GetById(CategoryId) != null;
        private bool IsDateTimeValid(DateTime StartDate , DateTime EndDate) => StartDate<EndDate&&DateTime.Now<StartDate;

       





        #endregion
    }
}
