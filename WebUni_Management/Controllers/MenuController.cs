using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebUni_Management.Attributes;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Menu;

namespace WebUni_Management.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService _menuService)
        {
            menuService = _menuService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await menuService.GetMenuAsync();
            return View(model);
        }

        [WordDocument(DefaultFilename = "MenuDocument")]
        public async Task<IActionResult> DownloadMenu()
        {
            var model = await menuService.GetMenuAsync();
            return View("DownloadMenu", model);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDish(int id)
        {
            if (await menuService.DishExistsById(id) == false)
            {
                return BadRequest();
            }
			var model = await menuService.GetDishForEditAsync(id);
            return View(model);
		}

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDish(int id, DishFormViewModel model)
        {
			if (await menuService.DishExistsById(id) == false)
            {
				return BadRequest();
			}
			if (ModelState.IsValid == false)
            {
				return View(model);
			}
			await menuService.EditDishAsync(id, model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeDate(int id)
        {
            if (await menuService.MenuExistsById(id) == false)
            {
				return BadRequest();
			}
			await menuService.ChangeMenuDateAsync(id);
			return RedirectToAction(nameof(Index));
        }
	}
}
