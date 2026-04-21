

namespace GymeManagementDAL.Entities
{
    public class Member:GymeUser
    {
        #region Properties
        public string Photo { get; set; } = null!;
        #endregion

        #region Member-HealthRecord
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - MemberShip
        public ICollection<MemberShip> memberShips { get; set; } = null!;
        #endregion

        #region member - MemberSessions
        public ICollection<MemberSessions> MemberSessions { get; set; } = null!;
        #endregion

    }
}
