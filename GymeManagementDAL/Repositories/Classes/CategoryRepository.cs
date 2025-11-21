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
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymeDbContext dbContext;
        public CategoryRepository(GymeDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public int Add(Category Category)
        {
            dbContext.Categories.Add(Category);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Category = dbContext.Categories.Find(id);
            if (Category == null) return 0;

            dbContext.Categories.Remove(Category);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => dbContext.Categories.ToList();


        public Category? GetById(int id) => dbContext.Categories.Find(id);

        public int Update(Category Category)
        {
            dbContext.Categories.Update(Category);
            return dbContext.SaveChanges();
        }
    }
}
