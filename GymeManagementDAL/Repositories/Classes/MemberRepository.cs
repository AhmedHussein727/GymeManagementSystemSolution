using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;


namespace GymeManagementDAL.Repositories.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymeDbContext dbContext;
        public MemberRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(Member member)
        {
            dbContext.Members.Add(member);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var member = dbContext.Members.Find(id);
            if(member == null)return 0;

            dbContext.Members.Remove(member);
           return dbContext.SaveChanges();
        }

        public IQueryable<Member> GetAll()=> dbContext.Members;
        

        public Member? GetById(int id)=>dbContext.Members.Find(id);

        public int Update(Member member)
        {
            dbContext.Members.Update(member);
            return dbContext.SaveChanges();
        }
    }
}
