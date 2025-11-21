using GymeManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CraeteMemberViewModel CreatedMember);

        MemberViewModel? GetMemberDetails(int  MemberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel MemberToUpdate);

        bool RemoveMember (int MemberId);
    }
}
