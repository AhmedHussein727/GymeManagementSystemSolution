using GymeManagementDAL.Entities;


namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface IMemberRepository
    {
        IQueryable<Member> GetAll();

        Member? GetById(int id);

        int Add(Member member);
        int Update(Member member);  
        int Delete(int id);



    }
}
