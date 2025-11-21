using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Entities
{
    public class MemberShip:BaseEntity
    {
        public DateTime EndDate { get; set; }
        public string Status { 
            get {
                if (EndDate >= DateTime.Now)
                    return "Active";
                else
                    return "Expired";
                } }
        public Member member { get; set; } = null!;
        public int MemberId { get; set; }

        public Plan Plan { get; set; }=null!;

        public int PlanId { get; set; }


    }
}
