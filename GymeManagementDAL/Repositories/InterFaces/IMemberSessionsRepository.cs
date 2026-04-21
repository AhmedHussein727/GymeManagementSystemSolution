using GymeManagementDAL.Entities;


namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface IMemberSessionsRepository
    {
        IQueryable<MemberSessions> GetAll();

        MemberSessions? GetById(int id);

        int Add(MemberSessions MemberSessions);
        int Update(MemberSessions MemberSessions);
        int Delete(int id);
    }
}
