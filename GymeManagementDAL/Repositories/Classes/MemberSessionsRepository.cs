using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;


namespace GymeManagementDAL.Repositories.Classes
{
    public class MemberSessionsRepository : IMemberSessionsRepository
    {
        private readonly GymeDbContext dbContext;
        public MemberSessionsRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(MemberSessions MemberSessions)
        {
            dbContext.MemberSessions.Add(MemberSessions);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var MemberSessions = dbContext.MemberSessions.Find(id);
            if (MemberSessions == null) return 0;

            dbContext.MemberSessions.Remove(MemberSessions);
            return dbContext.SaveChanges();
        }

        public IQueryable<MemberSessions> GetAll() => dbContext.MemberSessions.AsTracking();


        public MemberSessions? GetById(int id) => dbContext.MemberSessions.Find(id);

        public int Update(MemberSessions MemberSessions)
        {
            dbContext.MemberSessions.Update(MemberSessions);
            return dbContext.SaveChanges();
        }
    }
}
