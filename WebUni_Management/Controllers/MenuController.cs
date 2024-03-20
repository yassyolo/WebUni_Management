using Microsoft.AspNetCore.Mvc;
using WebUni_Management.Attributes;
using WebUni_Management.Core.Contracts;

namespace WebUni_Management.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService _menuService)
        {
            menuService = _menuService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await menuService.AllMenuItemsAsync();
            return View(model);
        }

        [WordDocument(DefaultFilename = "MenuDocument")]
        public async Task<IActionResult> DownloadMenu()
        {
            var model = await menuService.AllMenuItemsAsync();
            return View("DownloadMenu", model);
        }
    }
}
