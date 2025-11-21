using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.Classes
{
    internal class MemberSessionsRepository : IMemberSessionsRepository
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

        public IEnumerable<MemberSessions> GetAll() => dbContext.MemberSessions.ToList();


        public MemberSessions? GetById(int id) => dbContext.MemberSessions.Find(id);

        public int Update(MemberSessions MemberSessions)
        {
            dbContext.MemberSessions.Update(MemberSessions);
            return dbContext.SaveChanges();
        }
    }
}
