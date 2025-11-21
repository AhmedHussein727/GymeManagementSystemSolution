using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    internal interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAll();

        Trainer? GetById(int id);

        int Add(Trainer Trainer);
        int Update(Trainer Trainer);
        int Delete(int id);
    }
}
