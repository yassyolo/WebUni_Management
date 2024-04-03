using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.PersonalInfo;


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
		public async Task<IActionResult> SearchStudentIndex()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> SearchStudent([FromQuery] SearchStudentViewModel query)
		{
			query.Student = null;
		   var model = await personalInfoService.FilterStudentAsync(query.SearchTerm);
		   query.Student = model.Student;
           return View(query);
		}
		public async Task<IActionResult> StudentDetails(int id)
		{
			if (await personalInfoService.StudentWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}
			var model = await personalInfoService.GetStudentDetailsByIdAsync(id);
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> EditSubject(int subjectId, int studentId)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
				return BadRequest();
			}
			if(await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
				return BadRequest();
			}
			var model = await personalInfoService.GetEditSubjectFormAsync(subjectId);
			return View(model);
		}
	}
}
