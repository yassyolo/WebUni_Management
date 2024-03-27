using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;


namespace WebUni_Management.Controllers
{
	public class PersonalInfoController : Controller
	{
		private readonly IPersonalInfoService personalInfoService;
        private readonly IEventService eventService;

        public PersonalInfoController(IPersonalInfoService _personalInfoService, IEventService _eventService)
        {
            personalInfoService = _personalInfoService;
			eventService = _eventService;
        }
        public IActionResult Index()
		{
			return View();
		}
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RentedBooks(string userId) 
		{ 
			if(await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }
			var model = await personalInfoService.MyRentedBooksAsync(userId);
			return View(model);
		}
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RemoveRent(int id)
		{
			if(await personalInfoService.RentedBookExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (await personalInfoService.UserWithIdHasRentedBookAsync(id, userId) == false)
			{
				return BadRequest();
			}
			await personalInfoService.RemoveBookRentAsync(id, userId);
			return RedirectToAction("RentedBooks", new { userId = userId });
		}
		public async Task<IActionResult> JoinedEvents(string userId)
		{
			/*if(await eventService.UserHasJoinedEventsAsync(userId) == false)
			{

			}*/
			var model = await eventService.JoinedEventsAsync(userId);
			return View(model);
		}
	}
}
