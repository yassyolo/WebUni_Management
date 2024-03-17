using Microsoft.AspNetCore.Mvc;
using WebUni_Management.Core.Contracts;

namespace WebUni_Management.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService libraryService;

        public LibraryController(ILibraryService _libraryService)
        {
            libraryService = _libraryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await libraryService.LastThreeBooksAsync();
            return View(model);
        }
    }
}
