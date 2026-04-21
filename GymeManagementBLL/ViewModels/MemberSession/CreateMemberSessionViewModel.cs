




using GymeManagementBLL.ViewModels.MemberViewModels;

namespace GymeManagementBLL.ViewModels.MemberSession
{
    public class CreateMemberSessionViewModel
    {
        public int MemberId { get; set; }   
        public int SessionId { get; set; }  

        public IEnumerable<MemberViewModel> Members { get; set; } = new List<MemberViewModel>();
    }
}
