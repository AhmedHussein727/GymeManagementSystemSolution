

using GymeManagementBLL.ViewModels.MemberViewModels;
using GymeManagementBLL.ViewModels.PlanViewModels;

namespace GymeManagementBLL.ViewModels.MembershipsViewModels
{
    public class CreateMembershipViewModel
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public List<MemberViewModel> Members { get; set; } = new();
        public List<PlanViewModel> Plans { get; set; } = new();
    }
}
