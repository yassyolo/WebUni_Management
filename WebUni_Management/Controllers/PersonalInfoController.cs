using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.PersonalInfo;
using WebUni_Management.Extenstions;


namespace WebUni_Management.Controllers
{
	[Authorize]
    public class PersonalInfoController : Controller
	{
		private readonly IPersonalInfoService personalInfoService;
        private readonly IEventService eventService;

        public PersonalInfoController(IPersonalInfoService _personalInfoService, IEventService _eventService)
        {
            personalInfoService = _personalInfoService;
			eventService = _eventService;
        }

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RentedBooks([FromQuery] MyRentedBooksViewModel query) 
		{ 
			var userId = User.GetId();
			if(await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }

			var model = await personalInfoService.MyRentedBooksAsync(userId, query.CurrentPage, query.BooksPerPage);
			query.TotalBooks = model.TotalBooks;
			query.Books = model.Books;

			return View(query);
		}

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RemoveRent(int id)
		{
			if(await personalInfoService.RentedBookExistsByIdAsync(id) == false)
			{
                return BadRequest();
            }
			var userId = User.GetId();
			if (await personalInfoService.UserWithIdHasRentedBookAsync(id, userId) == false)
			{
				throw new BookRentException("Book is not rented by you!");
			}

			await personalInfoService.RemoveBookRentAsync(id, userId);

			return RedirectToAction("RentedBooks", new { userId = userId });
		}

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> JoinedEvents([FromQuery] MyJoinedEventsViewModel query)
		{
			var userId = User.GetId();
			if (await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }

			var model = await personalInfoService.JoinedEventsAsync(userId, query.CurrentPage, query.EventsPerPage);
			query.TotalEvents = model.TotalEvents;
			query.Events = model.Events;

			return View(query);
		}

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RemoveJoin(int id)
		{
            var userId = User.GetId();
			if(await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }
			if(await personalInfoService.EventWithIdExists(id) == false)
			{
				return BadRequest();
			}
            if(await personalInfoService.UserHasJoinedEventWithIdAsync(userId) == false)
			{
                throw new JoinEventException("You have not joined this event!");
            }

			try
			{
                await personalInfoService.RemoveJoinAsync(id, userId);

                return RedirectToAction("JoinedEvents", new { userId = userId });
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RentedRooms([FromQuery] MyRentedRoomsViewModel query)
		{
            var stringId = User.GetId();
			if (await personalInfoService.UserWithIdExistsAsync(stringId) == false)
			{
                return BadRequest();
            }

            var model = await personalInfoService.MyRentedRoomsAsync(stringId, query.CurrentPage, query.RoomsPerPage);
			query.TotalRooms = model.TotalRooms;
			query.Rooms = model.Rooms;

			return View(query);
        }

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SearchStudentIndex()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> SearchStudent([FromQuery] SearchStudentViewModel query)
		{
		   query.Student = null;
		   var model = await personalInfoService.FilterStudentAsync(query.SearchTerm);
		   query.Student = model.Student;

           return View(query);
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StudentDetails(int id)
		{
			if (await personalInfoService.StudentWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}

			try
			{
                var model = await personalInfoService.GetStudentDetailsByIdAsync(id);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSubject(int subjectId, int studentId)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
				return BadRequest();
			}
			if(await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
				throw new StudentWithSubjectException("Student does not have this this subject!");
			}


			try
			{
                var model = await personalInfoService.GetEditSubjectFormAsync(subjectId, studentId);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int subjectId, int studentId, EditSubjectFormViewModel model)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
            {
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
            {
                throw new StudentWithSubjectException("Student does not have this this subject!");
            }
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }

			try
			{
                await personalInfoService.EditSubjectAsync(subjectId, model);
                return RedirectToAction(nameof(StudentDetails), new { id = studentId });
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageAttendance(int subjectId, int studentId)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
                throw new StudentWithSubjectException("Student does not have this this subject!");
            }

			try
			{
                var model = await personalInfoService.GetAttendanceRecordForStudentAsync(subjectId, studentId);

                return View(model);
            }
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> SearchFaculties([FromQuery] AllFacultiesViewModel query)
		{
			var model = await personalInfoService.FilterFacultiesAsync(query.SearchTerm, query.CurrentPage, query.FacultiesPerPage);
			query.Faculties = model.Faculties;
			query.TotalFaculties = model.TotalFaculties;

			return View(query);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public  IActionResult AddFaculty()
		{
			var model = new FacultyFormViewModel();

			return View(model);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFaculty(FacultyFormViewModel model)
		{
            if (ModelState.IsValid == false)
			{
                return BadRequest();
            }

            await personalInfoService.AddFacultyAsync(model);

            return RedirectToAction(nameof(SearchFaculties));
        }

		[AllowAnonymous]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
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

			try
			{
                await personalInfoService.EditFacultyAsync(id, model);
                return RedirectToAction(nameof(SearchFaculties));
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> SearchMajors([FromQuery] AllMajorsViewModel query)
		{
            var model = await personalInfoService.FilterMajorsAsync(query.SearchTerm, query.CurrentPage, query.MajorsPerPage);
            query.Majors = model.Majors;
            query.TotalMajors = model.TotalMajors;

            return View(query);
        }

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
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
		[Authorize(Roles = "Admin")]
		public IActionResult AddMajor()
		{
			var model = new MajorFormViewModel();

			return View(model);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMajor(MajorFormViewModel model)
		{
			if(ModelState.IsValid == false)
			{
                return BadRequest();
            }

		    try
	     	{
                await personalInfoService.AddMajorAsync(model);
                return RedirectToAction(nameof(SearchMajors));
            }
     		catch (NotFoundException ex)
		    {
				return BadRequest(ex.Message); 
			}            
        }

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddAttendance(int subjectId, int studentId)
		{
            if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
                throw new StudentWithSubjectException("Student does not have this this subject!");
            }

			try
			{
                var model = await personalInfoService.AddAttendanceAsync(subjectId, studentId);

                return RedirectToAction(nameof(ManageAttendance), new { subjectId = subjectId, studentId = studentId });
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> SeePersonalInfo()
		{
			var userId = User.GetId();
			if (await personalInfoService.UserWithIdExistsAsync(userId) == false)
			{
                return BadRequest();
            }

			try
			{
                var model = await personalInfoService.LoadPersonalInfoAsync(userId);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> SeeMyAttendance(int id)
		{
			var userId = User.GetId();
            if (await personalInfoService.UserWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var studentId = await personalInfoService.GetStudentIdByUserIdAsync(userId);
            if (await personalInfoService.SubjectWithIdExistsAsync(id) == false)
			{
                return BadRequest();
            }
            if (await personalInfoService.StudentHasSubjectAsync(id, studentId) == false)
			{
                throw new StudentWithSubjectException("You are not enrolled in this subject!");
            }

			try
			{
                var model = await personalInfoService.SeeMyAttendanceRecordAsync(id, userId);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
        }

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> SeeMySubjectDetails(int id)
		{
			var userId = User.GetId();
            if (await personalInfoService.UserWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var studentId = await personalInfoService.GetStudentIdByUserIdAsync(userId);
			if (await personalInfoService.SubjectWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (await personalInfoService.StudentHasSubjectAsync(id, studentId) == false)
			{
                throw new StudentWithSubjectException("You are not enrolled in this subject!");
            }

			try
			{
                var model = await personalInfoService.SeeMySubjectDetailsAsync(id, userId);
                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		public async Task<IActionResult> SeeFacultyDetails(int id, string previousPage)
		{
            if (await personalInfoService.FacultyExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }

			var model = await personalInfoService.GetFacultyDetailsAsync(id);
			ViewBag.PreviousPage = previousPage;

			return View(model);
        }

		[AllowAnonymous]
		public async Task<IActionResult> SeeMajorDetails(int id, string previousPage)
		{
			if (await personalInfoService.MajorExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}

			try
			{
				var model = await personalInfoService.GetMajorDetailsAsync(id);
				ViewBag.PreviousPage = previousPage;

				return View(model);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		public async Task<IActionResult> SeeSubjects(int id)
		{
			if (await personalInfoService.MajorExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}

			var model = await personalInfoService.GetSubjectsForMajorAsync(id);
			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditSubjectForMajor(int id)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}
			try
			{
                var model = await personalInfoService.GetEditSubjectFormForMajorAsync(id);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditSubjectForMajor(int id, EditSubjectFormViewModel model)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (ModelState.IsValid == false)
			{
                return BadRequest();
            }

			try
			{
                await personalInfoService.EditSubjectAsync(id, model);

                return RedirectToAction(nameof(SearchMajors));
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]	
		public async Task<IActionResult> SeeSubjectDetails(int id)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}

			try
			{
                var model = await personalInfoService.GetSubjectDetailsById(id);

                return View(model);
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ManageGrade(int subjectId, int studentId)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
				return BadRequest();
			}
			if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
				throw new StudentWithSubjectException("Student does not have this this subject!");
			}

			var model = await personalInfoService.GetGradeForStudentAsync(subjectId, studentId);

			return View(model);
		}

		[Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<IActionResult> ManageGrade(int subjectId, int studentId, SubjectGradeViewModel model)
		{
			if (await personalInfoService.SubjectWithIdExistsAsync(subjectId) == false)
			{
				return BadRequest();
			}
			if (await personalInfoService.StudentHasSubjectAsync(subjectId, studentId) == false)
			{
				throw new StudentWithSubjectException("Student does not have this this subject!");
			}
			if (ModelState.IsValid == false)
			{
				return BadRequest();
			}

			try
			{
                await personalInfoService.ManageGradeForStudentAsync(subjectId, studentId, model);

                return RedirectToAction(nameof(StudentDetails), new { id = studentId });
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddSubjectForStudent(int id)
		{
			if (await personalInfoService.StudentWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}

			var model = new EditSubjectFormViewModel();

			return View(model);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubjectForStudent(int id, EditSubjectFormViewModel model)
		{
			if (await personalInfoService.StudentWithIdExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (ModelState.IsValid == false)
			{
				return BadRequest();
			}

			try
			{
                var subject = await personalInfoService.AddSubjectForStudentAsync(id, model);
                var subjectId = subject.Id;
                var profesor = await personalInfoService.AddSubjectProfessorForStudentAsync(subjectId, model);
                var assistant = await personalInfoService.AddSubjectAssistantForStudentAsync(subjectId, model);
                await personalInfoService.AddFullSubjectForStudentAsync(subjectId, id, profesor.Id, assistant.Id);
                return RedirectToAction(nameof(StudentDetails), new { id = id });
            }
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[Authorize(Roles = "Student")]
		public async Task<IActionResult> RemoveRoomRent(int id)
		{
			if (await personalInfoService.RoomExistsById(id) == false)
			{
				return BadRequest();
			}
			if(await personalInfoService.UserWithIdHasRentedRoomAsync(id, User.GetId()) == false)
			{
                throw new RoomIsRentedException("Room is already rented by you!");
            }
			if(await personalInfoService.RoomIsAlreadyRentedAsync(id) == false)
			{
                throw new RoomIsRentedException("Room is already rented by someone else!");
            }

			await personalInfoService.RemoveRoomRentAsync(id, User.GetId());

			return RedirectToAction(nameof(RentedRooms));
		}

        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteFaculty(int id)
		{
			if (await personalInfoService.FacultyExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}
			await personalInfoService.DeleteFacultyAsync(id);
			return RedirectToAction(nameof(SearchFaculties));
		}
    }
}
