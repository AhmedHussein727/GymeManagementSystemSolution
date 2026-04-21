using GymeManagementBLL.ViewModels.SessionViewModels;


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
