using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Core.Models.Account;
using Microsoft.AspNetCore.Authentication;
using ZXing.Common;
using ZXing;
using System.Drawing.Imaging;

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
                    InitialPassword = model.InitialPassword,
                    
                };


                var result = await userManager.CreateAsync(user, model.InitialPassword);

                if (result.Succeeded) 
                {
                    await userManager.AddPasswordAsync(user, model.InitialPassword);
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
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            var normalizedUsername = userManager.NormalizeName(model.Username);
            var user = await userManager.FindByNameAsync(normalizedUsername);
            if (user != null && ModelState.IsValid)
            {
                if (!user.IsApproved)
                {
                    TempData["Alert"] = "You have not been approved yet!";
                    ModelState.AddModelError(string.Empty, "User not approved.");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded && await userManager.IsInRoleAsync(user, "Student"))
                {
                    return RedirectToAction(nameof(ManageAccount));
                }
                else if (result.Succeeded && await userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction(nameof(Requests));
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Username or Password.");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Alert"] = "You have logged out successfully!";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ManageAccount()
        {
            bool nullStudent = await accountService.GetStudentAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (nullStudent)
            {
                var model = new ManageAccountViewModel();
                return View(model);
            }
            else
            {
                var model = await accountService.FillManageAccountAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
				return View("FilledManageAccount", model);
			}           
        }
        [HttpPost]
        public async Task<IActionResult> ManageAccount(ManageAccountViewModel model)
        { 
            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user= await accountService.FindUserByIdAsync(userId);

                if(user == null)
                {
                    return RedirectToAction(nameof(Login));
                }
                await accountService.AddStudentAsync(userId, model);
            }
            else
            {
                ModelState.AddModelError("", "Invalid attempt in updating your account.");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Requests([FromQuery] AllRequestsViewModel query)
        {
            var model = await accountService.GetAllRequestsAsync(query.CurrentPage, query.RequestsPerPage);
            query.Requests = model.Requests;
            //var model = await accountService.GetRequestsAsync();
            return View(query);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveRequest(string username)
        {
            
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"Unable to load user with username '{username}'.");
            }

            user.IsApproved = true;

            if (username.StartsWith("0"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
            else if(username.StartsWith("1"))
            {
                //generate qrcode
                //await accountService.StoreQRCodeAsync(user);
                await userManager.AddToRoleAsync(user, "Student");
            }


            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred setting IsApproved for user with ID '{user.Id}'.");
            }

            return RedirectToAction(nameof(Requests)); 
        }

        [HttpPost]
        public async Task<IActionResult> DiscardRequest(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"Unable to load user with username '{username}'.");
            }

            var result = await userManager.DeleteAsync(user);

            
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred setting IsApproved for user with ID '{user.Id}'.");
            }

            return RedirectToAction(nameof(Requests));
        }
        [HttpGet]
        public async Task<IActionResult> EditAccount(int id)
        {
            if (await accountService.StudentExistsByIdAsync(id) == false)
            {
				return BadRequest();
			}
			var model = await accountService.GetEditAccountFormAsync(id);
			return View(model);
		}
        [HttpPost]
        public async Task<IActionResult> EditAccount(int id, ManageAccountViewModel model)
        {
            if (await accountService.StudentExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            await accountService.EditAccountAsync(id, model);
            return RedirectToAction(nameof(ManageAccount));
        }

    }
}
