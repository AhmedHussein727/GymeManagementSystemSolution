using GymeManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Entities
{
    public class Trainer:GymeUser
    {
        public Specialities Specialities { get; set; }

        public ICollection<Sessions> TrainerSessions { get; set; } = null!;

    }
}
