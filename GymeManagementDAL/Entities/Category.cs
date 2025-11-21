using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Entities
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        #region Category - Session
        public ICollection<Sessions> Sessions { get; set; } = null!;
        #endregion
    }
}
