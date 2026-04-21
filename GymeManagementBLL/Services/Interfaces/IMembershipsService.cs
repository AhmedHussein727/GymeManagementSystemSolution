

using GymeManagementBLL.ViewModels.MembershipsViewModels;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface IMembershipsService
    {
        IEnumerable<MembershipViewModel>GetAllActiveMemberships();
        bool CreateMembership(CreateMembershipViewModel createMembershipViewModel);

        bool Cancel(int MembershipId);
    }
}
