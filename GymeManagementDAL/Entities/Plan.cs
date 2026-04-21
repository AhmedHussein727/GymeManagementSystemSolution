
namespace GymeManagementDAL.Entities
{
    public class Plan:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }=null!;
        public int DurationDays { get; set; }
        public Decimal Price { get; set; }
        public bool IsActive { get; set; }

        #region plan - MemberShip
        public ICollection<MemberShip> memberShips { get; set; } = null!;
        #endregion
    }
}
