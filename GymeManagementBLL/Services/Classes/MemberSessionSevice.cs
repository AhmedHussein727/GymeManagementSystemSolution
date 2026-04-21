
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MemberSession;
using GymeManagementBLL.ViewModels.SessionViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;

namespace GymeManagementBLL.Services.Classes
{
    public class MemberSessionSevice : IMemberSessionSevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMemberSessionsRepository _memberSessionRebository;
        private readonly IMemberRepository _memberRepository;

        public MemberSessionSevice(IUnitOfWork unitOfWork,IMapper mapper,ISessionRepository sessionRepository,
            IMemberRepository memberRepository,IMemberSessionsRepository memberSessionRebository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionRepository = sessionRepository;
            _memberSessionRebository = memberSessionRebository;
            memberRepository = memberRepository;
        }

        public bool CreateMemberSession(CreateMemberSessionViewModel vm)
        {
            // 1️⃣ Check Member
            var member = _unitOfWork.GetRepository<Member>().GetById(vm.MemberId);
            if (member == null) return false;

            // 2️⃣ Check Session
            var session = _sessionRepository
                .GetAll()
                .Include(x => x.MemberSessions)
                .FirstOrDefault(x => x.Id == vm.SessionId);

            if (session == null) return false;

            // 3️⃣ Check Future Session
            if (session.StartDate <= DateTime.Now) return false;

            // 4️⃣ Check Capacity
            if (session.MemberSessions.Count >= session.Capacity) return false;

            // 5️⃣ Check Duplicate Booking
            var alreadyBooked = session.MemberSessions
                .Any(x => x.MemberId == vm.MemberId);

            if (alreadyBooked) return false;

            // 6️⃣ Check Active Membership
            var hasMembership = _unitOfWork.GetRepository<MemberShip>()
                .GetAll()
                .Any(x => x.MemberId == vm.MemberId && x.EndDate > DateTime.Now);

            if (!hasMembership) return false;

            // 7️⃣ Create Booking
            var memberSession = new MemberSessions
            {
                MemberId = vm.MemberId,
                SessionId = vm.SessionId,
                ISAttended = false
            };
            memberSession.ISAttended = false;
            _unitOfWork.GetRepository<MemberSessions>().Add(memberSession);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions=_unitOfWork.GetRepository<Sessions>().GetAll();
            if(sessions == null)return Enumerable.Empty<SessionViewModel>();
           var result= _mapper.Map<IEnumerable< SessionViewModel>>(sessions);
            foreach( var session in result)
                session.AvailableSlots=session.Capacity-_sessionRepository.GetCountOfBookedSlots(session.Id);
            return result;
        }

        public IEnumerable<MemberSessionViewModel> GetMembersForOnGoingSession(int sessionId)
        {
            var MembersForOnGoingSession = _memberSessionRebository
                 .GetAll()
                 .Where(x => x.SessionId == sessionId && x.sessions.StartDate<=DateTime.Now
                   && x.sessions.EndDate >= DateTime.Now)
                 .Include(x => x.Member)
                 .ToList();

            return _mapper.Map<IEnumerable<MemberSessionViewModel>>(MembersForOnGoingSession);



        }

        public IEnumerable<MemberSessionViewModel> GetMembersForUpcomingSession(int sessionId)
        {
            var MembersForUpcomingSession = _memberSessionRebository
                .GetAll()
                .Where(x => x.SessionId == sessionId && x.sessions.StartDate > DateTime.Now)
                .Include(x => x.Member)
                
                .ToList();

            return _mapper.Map<IEnumerable<MemberSessionViewModel>>(MembersForUpcomingSession);
        }

        public bool MarkAsAttended(int memberSessionId)
        {
            // 1️⃣ Get booking + session
            var memberSession =_memberSessionRebository
                .GetAll()
                .Include(x => x.sessions)
                .FirstOrDefault(x => x.Id == memberSessionId);

            if (memberSession == null)
                return false;

            // 2️⃣ Check already attended
            if (memberSession.ISAttended)
                return false;

            // 3️⃣ Check session time (Ongoing)
            var now = DateTime.Now;

            if (memberSession.sessions.StartDate > now ||
                memberSession.sessions.EndDate < now)
                return false;

            // 4️⃣ Mark attendance
            memberSession.ISAttended = true;

            _unitOfWork.GetRepository<MemberSessions>().Update(memberSession);

            return _unitOfWork.SaveChanges() > 0;
        }

        public bool Cancel(int MemberSessionId)
        {
            var membersession= _unitOfWork.GetRepository<MemberSessions>().GetById(MemberSessionId);
            if(membersession == null) return false;

            _unitOfWork.GetRepository<MemberSessions>().Delete(membersession);

            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
