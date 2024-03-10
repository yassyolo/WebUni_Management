using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Models.Account;

namespace WebUni_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(IAccountService accountService, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    IsApproved = false,
                    InitialPassword = model.InitialPassword
                };

                var result = await userManager.CreateAsync(user, model.InitialPassword);

                if (result.Succeeded) 
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                foreach(var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Invalid");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var normalizedUsername = userManager.NormalizeName(model.Username);
            var user = await userManager.FindByNameAsync(normalizedUsername);
            if (user != null)
            {
                if (!user.IsApproved)
                {
                    ModelState.AddModelError(string.Empty, "User not approved.");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Username or Password.");
            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
