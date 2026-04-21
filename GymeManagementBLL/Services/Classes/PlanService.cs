using AutoMapper;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.PlanViewModels;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;


namespace GymeManagementBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork { get; }

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans= UnitOfWork.GetRepository<Plan>().GetAll().ToList();
            if (plans == null || !plans.Any()) return [];
            var planViewModels = mapper.Map<IEnumerable< PlanViewModel>>(plans);
            return planViewModels;
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan=UnitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null) return null;
            return mapper.Map<PlanViewModel>(plan);
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = UnitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null||HasActiveMemberShip(PlanId)||plan.IsActive==false) return null;
            return mapper.Map<UpdatePlanViewModel>(plan);
        }

        public bool ToggleStatus(int PlanId)
        {
            var repo = UnitOfWork.GetRepository<Plan>();
            var Plan = repo.GetById(PlanId);
            if(Plan == null||HasActiveMemberShip(PlanId)) return false;

            Plan.IsActive= Plan.IsActive==true ? false : true;
            Plan.UpdatedAt = DateTime.Now;
            try
            {
                repo.Update(Plan);
                return UnitOfWork.SaveChanges()>0;
            }
            catch 
            {
                return false;
            }

        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            var plan = UnitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null||HasActiveMemberShip(PlanId)) return false;

            try
            {
                mapper.Map(UpdatedPlan, plan);

                UnitOfWork.GetRepository<Plan>().Update(plan);
                return UnitOfWork.SaveChanges() > 0;
            }
            catch 
            {

                return false;
            }

        }

        #region Helper
        private bool HasActiveMemberShip(int planID)
        {
            var HasActiveMemberShips = UnitOfWork.GetRepository<MemberShip>().GetAll(x => x.PlanId == planID && x.Status == "Active").Any();
            return HasActiveMemberShips;
        }

        #endregion
    }
}
