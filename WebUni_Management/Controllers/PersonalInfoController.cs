using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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
			var model = await personalInfoService.GetEditSubjectFormAsync(subjectId, studentId);
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditSubject(int subjectId, int studentId, EditSubjectFormViewModel model)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
            {
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }
            await personalInfoService.EditSubjectAsync(subjectId, model);
            return RedirectToAction(nameof(StudentDetails), new { id = studentId });
        }
		public async Task<IActionResult> ManageAttendance(int subjectId, int studentId)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
                return BadRequest();
            }
            var model = await personalInfoService.GetAttendanceRecordForStudentAsync(subjectId, studentId);
            return View(model);
        }
		[HttpGet]
		public async Task<IActionResult> SearchFaculties([FromQuery] AllFacultiesViewModel query)
		{
			var model = await personalInfoService.FilterFacultiesAsync(query.SearchTerm, query.CurrentPage, query.FacultiesPerPage);
			query.Faculties = model.Faculties;
			query.TotalFaculties = model.TotalFaculties;
			return View(query);
		}
		[HttpGet]
		public  IActionResult AddFaculty()
		{
			var model = new FacultyFormViewModel();
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AddFaculty(FacultyFormViewModel model)
		{
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }
            await personalInfoService.AddFacultyAsync(model);
            return RedirectToAction(nameof(SearchFaculties));
        }
		public async Task<IActionResult> SeeMajors(int id)
		{
			if (await personalInfoService.FacultyExistsByIdAsync(id)== false)
			{
				return BadRequest();
			}
			var model = await personalInfoService.GetMajorsForFacultyAsync(id);
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> EditFaculty(int id)
		{
            if (await personalInfoService.FacultyExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await personalInfoService.GetEditFacultyFormAsync(id);
            return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> EditFaculty(int id, FacultyFormViewModel model)
		{
            if (await personalInfoService.FacultyExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }
            await personalInfoService.EditFacultyAsync(id, model);
            return RedirectToAction(nameof(SearchFaculties));
        }
		[HttpGet]
		public async Task<IActionResult> SearchMajors([FromQuery] AllMajorsViewModel query)
		{
            var model = await personalInfoService.FilterMajorsAsync(query.SearchTerm, query.CurrentPage, query.MajorsPerPage);
            query.Majors = model.Majors;
            query.TotalMajors = model.TotalMajors;
            return View(query);
        }
		[HttpGet]
		public async Task<IActionResult> EditMajor(int id)
		{
            if (await personalInfoService.MajorExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
            var model = await personalInfoService.GetEditMajorFormAsync(id);
            return View(model);
        }
		[HttpPost]
		public async Task<IActionResult> EditMajor(int id, MajorFormViewModel model)
		{
            if (await personalInfoService.MajorExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }
            await personalInfoService.EditMajorAsync(id, model);
            return RedirectToAction(nameof(SearchMajors));
        }
		[HttpGet]
		public async Task<IActionResult> AddMajor()
		{
			var model = new MajorFormViewModel();
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AddMajor(MajorFormViewModel model)
		{
            if (ModelState.IsValid)
			{
				try
				{
                    await personalInfoService.AddMajorAsync(model);
                    return RedirectToAction(nameof(SearchMajors));
                }
				catch (Exception ex)
				{
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the major. Please try again later.");
                    return View(model);
                    
				}
			}
			else
			{
				return View(model);
			}
            
        }
		public async Task<IActionResult> AddAttendance(int subjectId, int studentId)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
                return BadRequest();
            }
			var model = await personalInfoService.AddAttendanceAsync(subjectId, studentId);
            return RedirectToAction(nameof(ManageAttendance), new { subjectId = subjectId, studentId = studentId });
        }
		public async Task<IActionResult> SeePersonalInfo()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }
			var model = await personalInfoService.LoadPersonalInfoAsync(userId);
			return View(model);
		}
	
	}
}
