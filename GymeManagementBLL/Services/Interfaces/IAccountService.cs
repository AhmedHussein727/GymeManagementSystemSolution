

using GymeManagementBLL.ViewModels.AccountViewModels;
using GymeManagementDAL.Entities;

namespace GymeManagementBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LoginViewModel loginViewModel);
    }
}
