using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();

        Plan? GetById(int id);

        int Add(Plan Plan);
        int Update(Plan Plan);
        int Delete(int id);
    }
}
