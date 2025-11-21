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
    internal class MembershipRepo : IMembershipRepository
    {
        private readonly GymeDbContext dbContext;
        public MembershipRepo(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(MemberShip MemberShip)
        {
            dbContext.MemberShips.Add(MemberShip);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var MemberShip = dbContext.MemberShips.Find(id);
            if (MemberShip == null) return 0;

            dbContext.MemberShips.Remove(MemberShip);
            return dbContext.SaveChanges();
        }

        public IEnumerable<MemberShip> GetAll() => dbContext.MemberShips.ToList();


        public MemberShip? GetById(int id) => dbContext.MemberShips.Find(id);

        public int Update(MemberShip MemberShip)
        {
            dbContext.MemberShips.Update(MemberShip);
            return dbContext.SaveChanges();
        }
    }
}
