using AutoMapper;
using GymeManagementBLL.Services.AttachmentService;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MemberViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;


namespace GymeManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        public IUnitOfWork UnitOfWork { get; }

        public MemberService(IUnitOfWork unitOfWork,IMapper mapper,IAttachmentService attachmentService)
        {
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var memberRepo = UnitOfWork.GetRepository<Member>();
            var members= memberRepo.GetAll();
            if (members == null) return [];

            var memberViewModels = mapper.Map<IEnumerable<MemberViewModel>>(members);
            return memberViewModels;
        }

        public bool CreateMember(CraeteMemberViewModel CreatedMember)
        {
            try
            {
               

                if (IsEmailExist(CreatedMember.Email) || IsPhoneExist(CreatedMember.Phone)) return false;

                var PhotoName = attachmentService.Upload("members", CreatedMember.photoFile);
                if(string.IsNullOrEmpty(PhotoName)) return false;

                var member = mapper.Map<Member>(CreatedMember);
                member.Photo=PhotoName;

                 UnitOfWork.GetRepository<Member>().Add(member) ;
               var IsCreated= UnitOfWork.SaveChanges()>0;
                if(!IsCreated)
                {
                    attachmentService.Delete(PhotoName, "members");
                    return false;
                }
                else
                {
                    return IsCreated;
                }
            }
            catch
            { 
                return false; 
            }
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member=UnitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            var ViewModel = mapper.Map<MemberViewModel>(member);

            var ActiveMemberShip = UnitOfWork.GetRepository<MemberShip>().GetAll(x => x.MemberId == MemberId&&x.Status=="Active").FirstOrDefault();
            if(ActiveMemberShip is not null)
            {
                ViewModel.MembershipStartDate=ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MembershipEndDate=ActiveMemberShip?.EndDate.ToShortDateString();
                var plan = UnitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = plan?.Name;
            }
            return ViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var healthRecord=UnitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (healthRecord == null) return null;

            return mapper.Map<HealthRecordViewModel>(healthRecord); 
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member=UnitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;
            return mapper.Map<MemberToUpdateViewModel>(member);
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                var EmailExists=UnitOfWork.GetRepository<Member>().GetAll(x=>x.Email==UpdatedMember.Email &&x.Id !=MemberId);
                var PhoneExists = UnitOfWork.GetRepository<Member>().GetAll(x => x.Phone == UpdatedMember.Phone && x.Id != MemberId);
                if(EmailExists.Any() || PhoneExists.Any() )return false;
                var memberRepo = UnitOfWork.GetRepository<Member>();
                
                var member = memberRepo.GetById(MemberId);
                if (member == null) return false;
                mapper.Map(UpdatedMember,member);
                 memberRepo.Update(member);
                return UnitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var memberRepo = UnitOfWork.GetRepository<Member>();

            var member = memberRepo.GetById(MemberId);
            var EmailExists = UnitOfWork.GetRepository<Member>().GetAll(x => x.Email == member.Email && x.Id != MemberId);
            var PhoneExists = UnitOfWork.GetRepository<Member>().GetAll(x => x.Phone == member.Phone && x.Id != MemberId);
            if (EmailExists.Any() || PhoneExists.Any()) return false;

            if (member == null) return false;
            var sessionsIds = UnitOfWork.GetRepository<MemberSessions>().GetAll(x => x.MemberID == MemberId).Select(x=>x.SessionID);
            var hasFutureSessions = UnitOfWork.GetRepository<Sessions>().GetAll(x => sessionsIds.Contains(x.Id) && x.StartDate > DateTime.Now).Any();
            if(hasFutureSessions) return false;
            var MemberShips = UnitOfWork.GetRepository<MemberShip>().GetAll(x => x.MemberId == MemberId);
            try
            {
                if(MemberShips.Any())
                {
                    foreach(var memberShip in MemberShips)
                    {
                        UnitOfWork.GetRepository<MemberShip>().Delete(memberShip);
                    }
                }
                 memberRepo.Delete(member);
                var IsDeleted= UnitOfWork.SaveChanges() > 0;
                if(IsDeleted)
                    attachmentService.Delete(member.Photo, "members");
                return IsDeleted;
                
            }
            catch
            {
                return false;
            }
        }


        #region Helper Methods
        bool IsEmailExist(string Email)
        {
            return UnitOfWork.GetRepository<Member>().GetAll(x=>x.Email==Email).Any();
        }
        bool IsPhoneExist(string Phone)
        {
            return UnitOfWork.GetRepository<Member>().GetAll(x => x.Phone == Phone).Any();
        }

       
        #endregion
    }
}
