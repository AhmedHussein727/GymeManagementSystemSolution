using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    internal interface IMembershipRepository
    {
        IEnumerable<MemberShip> GetAll();

        MemberShip? GetById(int id);

        int Add(MemberShip MemberShip);
        int Update(MemberShip MemberShip);
        int Delete(int id);
    }
}
