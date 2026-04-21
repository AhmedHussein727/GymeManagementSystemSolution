

namespace GymeManagementDAL.Entities
{
    public class Sessions:BaseEntity
    {
        #region Properties
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        #endregion


        #region Session - Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }= null!;
        #endregion

        #region Session - Trainer
        public Trainer Trainer { get; set; }=null!;
        public int TrainerId { get; set; }
        #endregion

        #region session - MemberSessions
        public ICollection<MemberSessions> MemberSessions { get; set; } = null!;
        #endregion


    }
}
