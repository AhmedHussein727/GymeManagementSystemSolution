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
    public class TrainerRepository : ITrainerRepository
    {

        private readonly GymeDbContext dbContext;
        public TrainerRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public int Add(Trainer Trainer)
        {
            dbContext.Add(Trainer);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Trainers = dbContext.Trainers.Find(id);
            if (Trainers == null) return 0;

            dbContext.Trainers.Remove(Trainers);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => dbContext.Trainers.ToList();


        public Trainer? GetById(int id) => dbContext.Trainers.Find(id);

        public int Update(Trainer Trainers)
        {
            dbContext.Trainers.Update(Trainers);
            return dbContext.SaveChanges();
        }
    }
}
