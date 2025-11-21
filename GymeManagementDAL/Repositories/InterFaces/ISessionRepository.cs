using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface ISessionRepository
    {
        IEnumerable<Sessions> GetAll();

        Sessions? GetById(int id);

        int Add(Sessions Session);
        int Update(Sessions Session);
        int Delete(int id);
        IEnumerable<Sessions> GetAllSessionsWithTrainersAndCategory();
        int GetCountOfBookedSlots(int SessionId);
        Sessions? GetSessionWithTrainerAndCategory(int SessionId);
    }
}
