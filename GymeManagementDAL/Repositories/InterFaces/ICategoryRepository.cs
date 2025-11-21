using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Repositories.InterFaces
{
    internal interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category? GetById(int id);

        int Add(Category Category);
        int Update(Category Category);
        int Delete(int id);
    }
}
