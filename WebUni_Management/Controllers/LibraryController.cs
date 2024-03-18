using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Library;

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
            var model = new LibraryInfoViewModel();
            model.BookInfoViewModel = await libraryService.LastThreeBooksAsync();
            model.StudyRoomInfo = await libraryService.LastThreeStudyRoomsAsync();
            return View(model);
        }
        public async Task<IActionResult> AllShowcase([FromQuery]AllBooksQueryModel query)
        {
            var model = await libraryService.AllBooksAsync(query.Category, query.SearchTerm, query.CurrentPage, query.BooksPerPage);

            query.TotalBooksCount = model.TotalBooksCount;
            query.Books = model.Books;
            query.Categories = await libraryService.AllCategorisNamesAsync();
            return View(query);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if( await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await libraryService.BookDetailsAsync();
            return View(model);
        }
        public async Task<IActionResult> Rent(int id)
        {
			if (await libraryService.BookExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}
            if(await libraryService.IsBookRentedAsync(id) == true)
            {
				return BadRequest();
			}
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await libraryService.RentBookAsync(id, userId);
            return RedirectToAction("RentedBooks", "PersonalInfo", new { userId = userId });
        }
    }
}
