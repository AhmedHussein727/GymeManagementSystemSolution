using GymeManagementBLL.Services.Interfaces;
using GymeManagementBLL.ViewModels.AccountViewModels;
using GymeManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymeManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService,SignInManager<ApplicationUser>signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            var user = accountService.ValidateUser(loginViewModel);
            if (user is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email or password");
                return View(loginViewModel);
            }
            var result = signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Your Account is not Allowed");
            }
            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account is locked out");
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(loginViewModel);

        }
        #endregion

        #region Logout
        [HttpPost]
        public ActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Access Denied
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
