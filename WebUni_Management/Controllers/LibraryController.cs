using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<IActionResult> AllBooksShowcase([FromQuery] AllBooksQueryModel query)
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
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await libraryService.BookDetailsAsync(id);
            return View(model);
        }
        public async Task<IActionResult> Rent(int id)
        {
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if (await libraryService.IsBookRentedAsync(id) == true)
            {
                return BadRequest();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await libraryService.RentBookAsync(id, userId);
            return RedirectToAction("RentedBooks", "PersonalInfo", new { userId = userId });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await libraryService.GetEditFormBookModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel model)
        {
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if(await libraryService.CategoryExistsById(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category not found");
            }
            if(ModelState.IsValid == false)
            {
                model.Categories = await libraryService.AllCategoriesForEditAsync();
                return View(model);
            }
            await libraryService.EditBookAsync(id,model);
            return RedirectToAction(nameof(AllBooksShowcase));
        }
        [HttpGet]
        public async Task< IActionResult> AllRoomsShowcase([FromQuery] AllRoomsQueryModel query)
        {
            var model = await libraryService.AllRoomsAsync(query.Capacity, query.SearchTerm, query.CurrentPage, query.RoomsPerPage);

            query.TotalRooms = model.TotalRooms;
            query.StudyRooms = model.StudyRooms;

            return View(model);
        }
        public async Task<IActionResult> DetailsRoom(int id)
        {
            if(await libraryService.RoomExistsByIdAsync(id) == false)
            {
                return BadRequest(); 
            }

            var model = await libraryService.GetRoomDetailsByIdAsync(id);
            return View(model);
        }
        public async Task<IActionResult> EditRoom(int id)
        {
            if (await libraryService.RoomExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await libraryService.GetEditRoomAsync(id);
            return View(model);
        }
        public IActionResult ManageIndex()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var model = new EditBookViewModel();
            model.Categories = await libraryService.AllCategoriesForEditAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(EditBookViewModel model)
        {

            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }
            if (await libraryService.CategoryExistsById(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category not found");
            }
            await libraryService.AddBookAsync(model);
            return RedirectToAction(nameof(AllBooksShowcase));
        }


    }
}
