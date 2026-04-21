

namespace GymeManagementDAL.Entities
{
    public class MemberSessions : BaseEntity
    {
        public bool ISAttended { get; set; }
        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }

        public Sessions sessions { get; set; }=null!;
        public int SessionId { get; set; }
    }
}
