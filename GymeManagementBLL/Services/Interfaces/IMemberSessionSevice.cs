using GymeManagementBLL.ViewModels.MemberSession;
using GymeManagementBLL.ViewModels.SessionViewModels;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface IMemberSessionSevice
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        public bool CreateMemberSession(CreateMemberSessionViewModel VModel);

        IEnumerable<MemberSessionViewModel> GetMembersForUpcomingSession(int sessionId);

        IEnumerable<MemberSessionViewModel> GetMembersForOnGoingSession(int sessionId);

        public bool MarkAsAttended(int memberSessionId);

        public bool Cancel(int MemberSessionId);


    }
}
