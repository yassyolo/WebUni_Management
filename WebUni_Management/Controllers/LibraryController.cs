using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;

using WebUni_Management.Core.Models.Library;
using WebUni_Management.Extenstions;

namespace WebUni_Management.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryService libraryService;

        public LibraryController(ILibraryService _libraryService)
        {
            libraryService = _libraryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new LibraryInfoViewModel();
                model.BookInfoViewModel = await libraryService.LastThreeBooksAsync();
                model.StudyRoomInfo = await libraryService.LastThreeStudyRoomsAsync();
                return View(model);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllBooksShowcase([FromQuery] AllBooksQueryModel query)
        {
            var model = await libraryService.AllBooksAsync(query.Category, query.SearchTerm, query.CurrentPage, query.BooksPerPage);

            query.TotalBooksCount = model.TotalBooksCount;
            query.Books = model.Books;
            query.Categories = await libraryService.AllCategorisNamesAsync();

            return View(query);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string previousPage)
        {
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await libraryService.BookDetailsAsync(id);
            ViewBag.PreviousPage = previousPage;

            return View(model);
        }

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> Rent(int id)
        {
            if (await libraryService.BookExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if (await libraryService.IsBookRentedAsync(id) == true)
            {
                throw new BookRentException("Book is already rented");
            }
            if(await libraryService.IsBookRentedByUserWithIdAsync(User.GetId(), id) == true)
            {
                throw new BookRentException("Book is already rented by you");
            }

            string userId = User.GetId();
            await libraryService.RentBookAsync(id, userId);

            return RedirectToAction("RentedBooks", "PersonalInfo", new { userId = userId });
        }

        [HttpGet]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
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
        [AllowAnonymous]
        public async Task< IActionResult> AllRoomsShowcase([FromQuery] AllRoomsQueryModel query)
        {
            var model = await libraryService.AllRoomsAsync(query.Capacity, query.SearchTerm, query.CurrentPage, query.RoomsPerPage);

            query.TotalRooms = model.TotalRooms;
            query.StudyRooms = model.StudyRooms;

            return View(query);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DetailsRoom(int id, string previousPage)
        {
            if(await libraryService.RoomExistsByIdAsync(id) == false)
            {
                return BadRequest(); 
            }

            var model = await libraryService.GetRoomDetailsByIdAsync(id);
            ViewBag.PreviousPage = previousPage;

            return View(model);
        }

		[Authorize(Roles = "Admin")]
        [HttpGet]
		public async Task<IActionResult> EditRoom(int id)
        {
            if (await libraryService.RoomExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await libraryService.GetEditRoomAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditRoomViewModel model)
        {
			if (await libraryService.RoomExistsByIdAsync(id) == false)
            {
				return BadRequest();
			}
			if (ModelState.IsValid == false)
            {
				return View(model);
			}

			await libraryService.EditRoomAsync(id, model);

			return RedirectToAction(nameof(AllRoomsShowcase));
		}

		[Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult ManageIndex()
        {
            var message = TempData["Alert"];

            return View();
        }

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddBook()
        {
            var model = new EditBookViewModel();
            model.Categories = await libraryService.AllCategoriesForEditAsync();

            return View(model);
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
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

            var bookId = await libraryService.AddBookAsync(model);
            var authors = await libraryService.AddAuthorsForNewBookAsync(bookId, model);
            await libraryService.AddBookByAuthorsAsync(bookId, authors);

            return RedirectToAction(nameof(AllBooksShowcase));
        }

        [HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult AddRoom()
        {
            var model = new EditRoomViewModel();

            return View(model);
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddRoom(EditRoomViewModel model)
        {
            if(ModelState.IsValid == false)
            {
                return View(model);
            }

            await libraryService.AddRoomAsync(model);

            return RedirectToAction(nameof(ManageIndex));
        }
      
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ManageBookRent()
        {
            var model = await libraryService.ManageBookRentAsync();

            TempData["Alert"] = $"{model.TotalBookRented} Book Items Rent managed successfully!";

            return RedirectToAction(nameof(ManageIndex));
        }

		[Authorize(Roles = "Admin")]       
		public async Task<IActionResult> ManageRoomRent()
		{
			var model = await libraryService.ManageRoomRentAsync();

			TempData["Alert"] = $"{model.TotalRoomsRented} Room Items Rent managed successfully!";

			return RedirectToAction(nameof(ManageIndex));
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
			if (await libraryService.BookExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}

            await libraryService.DeleteBookAsync(id);

            return RedirectToAction(nameof(AllBooksShowcase));
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
			if (await libraryService.RoomExistsByIdAsync(id) == false)
            {
				return BadRequest();
			}

			await libraryService.DeleteRoomAsync(id);

			return RedirectToAction(nameof(AllRoomsShowcase));
		}

        public async Task<IActionResult> RentRoom(int id)
        {
            var userId = User.GetId();
            if (await libraryService.RoomExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if(await libraryService.IsRoomRentedAsync(id) == true)
            {
                throw new RoomIsRentedException("Room is already rented!");
            }
            if(await libraryService.IsRoomRentedByUserWithIdAsync(userId, id) == true)
            {
                throw new RoomIsRentedException("Room is already rented by you!");
            }
            
            await libraryService.RentRoomAsync(userId, id);
            return RedirectToAction("RentedRooms", "PersonalInfo");
        }

	}
}
