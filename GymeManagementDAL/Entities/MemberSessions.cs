using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Entities
{
    public class MemberSessions : BaseEntity
    {
        public bool ISAttended { get; set; }
        public Member Member { get; set; } = null!;
        public int MemberID { get; set; }

        public Sessions sessions { get; set; }=null!;
        public int SessionID { get; set; }
    }
}
