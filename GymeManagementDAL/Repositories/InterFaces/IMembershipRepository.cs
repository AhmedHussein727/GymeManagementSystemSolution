using GymeManagementDAL.Entities;


namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface IMembershipRepository
    {
        IQueryable<MemberShip> GetAll();

        MemberShip? GetById(int id);

        int Add(MemberShip MemberShip);
        int Update(MemberShip MemberShip);
        int Delete(int id);
    }
}
