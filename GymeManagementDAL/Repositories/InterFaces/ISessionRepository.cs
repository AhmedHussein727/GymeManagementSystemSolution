using GymeManagementDAL.Entities;


namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface ISessionRepository
    {
        IQueryable<Sessions> GetAll();

        Sessions? GetById(int id);

        int Add(Sessions Session);
        int Update(Sessions Session);
        int Delete(int id);
        IEnumerable<Sessions> GetAllSessionsWithTrainersAndCategory();
        int GetCountOfBookedSlots(int SessionId);
        Sessions? GetSessionWithTrainerAndCategory(int SessionId);
    }
}
