using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;


namespace GymeManagementDAL.Repositories.Classes
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GymeDbContext dbContext;
        public SessionRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(Sessions Session)
        {
            dbContext.Sessions.Add(Session);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Sessions = dbContext.Sessions.Find(id);
            if (Sessions == null) return 0;

            dbContext.Sessions.Remove(Sessions);
            return dbContext.SaveChanges();
        }

        public IQueryable<Sessions> GetAll() => dbContext.Sessions.AsNoTracking();

        public IEnumerable<Sessions> GetAllSessionsWithTrainersAndCategory()
        {
            return dbContext.Sessions.Include(x => x.Trainer)
                                         .Include(x => x.Category)
                                         .ToList();
        }

        public Sessions? GetById(int id) => dbContext.Sessions.Find(id);

        public int GetCountOfBookedSlots(int SessionId)
        {
            return dbContext.MemberSessions
          .Include(x => x.sessions)
          .Count(x => x.SessionId == SessionId
             && x.sessions.EndDate > DateTime.Now);
        }

        public Sessions? GetSessionWithTrainerAndCategory(int SessionId)
        {
            return dbContext.Sessions.Include(x => x.Trainer).Include(x => x.Category).FirstOrDefault(x=>x.Id==SessionId);
        }

        public int Update(Sessions Sessions)
        {
            dbContext.Sessions.Update(Sessions);
            return dbContext.SaveChanges();
        }
    }
}
