using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymeDbContext dbContext;
        public PlanRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(Plan Plan)
        {
            dbContext.Plans.Add(Plan);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Plans = dbContext.Plans.Find(id);
            if (Plans == null) return 0;

            dbContext.Plans.Remove(Plans);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => dbContext.Plans.ToList();


        public Plan? GetById(int id) => dbContext.Plans.Find(id);

        public int Update(Plan Plans)
        {
            dbContext.Plans.Update(Plans);
            return dbContext.SaveChanges();
        }
    }
}
