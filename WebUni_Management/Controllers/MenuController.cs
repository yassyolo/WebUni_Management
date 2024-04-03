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
            var model = await menuService.GetMenuAsync();
            return View(model);
        }

        [WordDocument(DefaultFilename = "MenuDocument")]
        public async Task<IActionResult> DownloadMenu()
        {
            var model = await menuService.GetMenuAsync();
            return View("DownloadMenu", model);
        }
        /*public async Task<IActionResult> UpdateMenu()
        {
            var model = await menuService.GetMenuFormForUpdateAsync();
            return View(model);
        }*/
    }
}
