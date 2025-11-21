using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    internal interface IMemberSessionsRepository
    {
        IEnumerable<MemberSessions> GetAll();

        MemberSessions? GetById(int id);

        int Add(MemberSessions MemberSessions);
        int Update(MemberSessions MemberSessions);
        int Delete(int id);
    }
}
