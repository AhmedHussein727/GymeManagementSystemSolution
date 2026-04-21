using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;


namespace GymeManagementDAL.Repositories.Classes
{
    public class MembershipRepo : IMembershipRepository
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

        public IQueryable<MemberShip> GetAll() => dbContext.MemberShips;


        public MemberShip? GetById(int id) => dbContext.MemberShips.Find(id);

        public int Update(MemberShip MemberShip)
        {
            dbContext.MemberShips.Update(MemberShip);
            return dbContext.SaveChanges();
        }
    }
}
