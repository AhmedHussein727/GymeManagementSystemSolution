using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymeManagementDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData(GymeDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasPlans || HasCategories) return false;
                if (!HasPlans)
                {
                    var plans = LoadDataFromJsonFiles<Plan>("plans.json");
                    if (plans.Any())
                        dbContext.Plans.AddRange(plans);
                }
                if (!HasCategories)
                {
                    var categories = LoadDataFromJsonFiles<Category>("categories.json");
                    if (categories.Any())
                        dbContext.Categories.AddRange(categories);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Feeld {ex}");
                return false;
                
            }
        }

        private static List<T>LoadDataFromJsonFiles<T>(string fileName)
        {
            var filePath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",fileName);
            if (!File.Exists(filePath))throw new FileNotFoundException();
            string Data=File.ReadAllText(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(Data, options)??new List<T>();
        }
    }
}
