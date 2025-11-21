using GymeManagementBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int SessionId);
        bool CteateSession(CreateSessionViewModel CreatedSession);

        UpdateSessionViewModel? GetSessionToUpdate(int SessionId);

        bool UpdateSession(UpdateSessionViewModel UpdatedSession,int SessionId);
        bool RemoveSession(int SessionId);

        IEnumerable<TrainerSelectViewModel> GetTrainersForDropDowen();
        IEnumerable<CategorySelectViewModel> GetCategoriesForDropDowen();

    }
}
