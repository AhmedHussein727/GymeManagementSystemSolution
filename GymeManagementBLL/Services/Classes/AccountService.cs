

using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.AccountViewModels;
using GymeManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymeManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
            var user=userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user == null) return null;
            var isPasswordValid=userManager.CheckPasswordAsync(user,loginViewModel.Password).Result;
            return isPasswordValid ? user : null;
        }
    }
}
