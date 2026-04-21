

namespace GymeManagementBLL.ViewModels.MembershipsViewModels
{
    public class MembershipViewModel
    {
        public int Id { get; set; }
        public string MemberName { get; set; } = default!;

        public string PlanName { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { set; get; }


    }
}
