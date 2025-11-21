using AutoMapper;
using AutoMapper.Execution;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.TrainerViewMofels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork { get; }

        public TrainerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public bool CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if(IsEmailExist(createdTrainer.Email)||IsPhoneExist(createdTrainer.Phone)||HasFutureSessions(createdTrainer.Id)) return false;
            var trainer =mapper.Map<Trainer>(createdTrainer);
            trainer.CreatedAt = DateTime.Now;
            try
            {
                UnitOfWork.GetRepository<Trainer>().Add(trainer);
                return UnitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<TrainerViewModel> GetAllTeainers()
        {
            var trainers= UnitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null || !trainers.Any()) return [];
            var trainersViewModel = mapper.Map<IEnumerable< TrainerViewModel>>(trainers);
            return trainersViewModel;
        }

        public TrainerViewModel? GetTrainerDetails(int TrainerID)
        {
            var trainer= UnitOfWork.GetRepository<Trainer>().GetById(TrainerID);
            if (trainer == null) return null;
            return mapper.Map<TrainerViewModel>(trainer);
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerID)
        {
            var trainer = UnitOfWork.GetRepository<Trainer>().GetById(TrainerID);
            if (trainer == null) return null;
            return mapper.Map<TrainerToUpdateViewModel>(trainer);
        }

        public bool RemoveTrainer(int TrainerID)
        {
            var trainer = UnitOfWork.GetRepository<Trainer>().GetById(TrainerID);
            var EmailExists = UnitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == trainer.Email && x.Id != TrainerID);
            var PhoneExists = UnitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == trainer.Phone && x.Id != TrainerID);
            if (EmailExists.Any() || PhoneExists.Any()) return false;
            try
            {
                UnitOfWork.GetRepository<Trainer>().Delete(trainer);
                return UnitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            var trainer= UnitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return false;
            var EmailExists = UnitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == trainer.Email && x.Id != TrainerId);
            var PhoneExists = UnitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == trainer.Phone && x.Id != TrainerId);
            mapper.Map(UpdatedTrainer, trainer);
            try
            {
                UnitOfWork.GetRepository<Trainer>().Update(trainer);
               return UnitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper
        private bool IsEmailExist(string Email)
        {
            return UnitOfWork.GetRepository<Trainer>().GetAll(x=>x.Email == Email).Any();
        }
        private bool IsPhoneExist(string Phone)
        {
            return UnitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == Phone).Any();
        }
        private bool HasFutureSessions(int TrainerId)
        {
            return UnitOfWork.GetRepository<Sessions>().GetAll(x=>x.TrainerId==TrainerId&&x.StartDate>DateTime.Now).Any();
        }
        #endregion
    }
}
