
using GymeManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymeManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers=userManager.Users.Any();
                var HasRols=roleManager.Roles.Any();
                if (HasUsers && HasRols) return false;

                if(!HasRols)
                {
                    var roles = new List<IdentityRole>()
                    {
                        new(){Name="SuperAdmin"},
                        new(){Name="Admin"}
                    };

                    foreach (var Role in roles)
                    {
                        if(!roleManager.RoleExistsAsync(Role.Name).Result)
                        {
                            roleManager.CreateAsync(Role).Wait();
                        }

                    }

                }

                if(!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Ahmed",
                        LastName = "mohamed",
                        UserName = "AhmedMohamed",
                        Email = "AhmedMohamed@gmail.com",
                        PhoneNumber = "01522982704"
                    };
                    userManager.CreateAsync(MainAdmin,"P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin,"SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Ali",
                        LastName = "Gomaa",
                        UserName = "AliGomaa",
                        Email = "AliGomaa@gmail.com",
                        PhoneNumber = "01522985479"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
