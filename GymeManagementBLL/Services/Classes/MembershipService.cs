
using AutoMapper;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.MembershipsViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GymeManagementBLL.Services.Classes
{
    public class MembershipService : IMembershipsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMembershipRepository _membershipRebository;

        public MembershipService(IUnitOfWork unitOfWork,IMapper mapper, IMembershipRepository membershipRebository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _membershipRebository = membershipRebository;
        }

    

        public IEnumerable<MembershipViewModel> GetAllActiveMemberships()
        {
            var memberships =_membershipRebository
                
                .GetAll()
                .Include(x => x.member)
                .Include(x => x.Plan)
                .Where(x => x.EndDate > DateTime.Now)
                .ToList();
            
            if(!memberships.Any() ) return [];
            
            var planViewModels=_mapper.Map<IEnumerable< MembershipViewModel>>(memberships);

            return planViewModels;
            
        }

        public bool CreateMembership(CreateMembershipViewModel createMembershipViewModel)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(createMembershipViewModel.MemberId);
            var plan=_unitOfWork.GetRepository<Plan>().GetById(createMembershipViewModel.PlanId);
            if(member == null ) return false;
            if(plan == null || !plan.IsActive) return false;

            var haveActiveMimbership=_unitOfWork.GetRepository<MemberShip>().GetAll()
                .Any(x=>x.MemberId==createMembershipViewModel.MemberId && x.EndDate > DateTime.Now);

            if(haveActiveMimbership) return false;

            var membership=_mapper.Map<MemberShip>(createMembershipViewModel);

            membership.CreatedAt = DateTime.Now;
            membership.EndDate = DateTime.Now.AddDays(plan.DurationDays);

            _unitOfWork.GetRepository<MemberShip>().Add(membership);
            _unitOfWork.SaveChanges();

            return true;


        }

        public bool Cancel(int MembershipId)
        {
            var membership=_unitOfWork.GetRepository<MemberShip>().GetById(MembershipId);
            if(membership == null) return false;
            if(membership.EndDate <= DateTime.Now) return false;
            _unitOfWork.GetRepository<MemberShip>().Delete(membership);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
