using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.PersonalInfo;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUnii_Management.Tests
{
	[TestFixture]
	public class PersonalInfoService_Tests
	{
		private IPersonalInfoService personalInfoService;
		private IRepository repository;
		private ApplicationDbContext context;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "LibraryService_Tests")
				.Options;

			context = new ApplicationDbContext(options);

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}
		[Test]
		public async Task FilterStudentAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Student
			{
				Id = 20,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FilterStudentAsync(null);

			Assert.AreEqual(null, result);

		}
		[Test]
		public async Task FilterStudentAsync_ShouldReturnModelWithCorrectData()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Student
			{
				Id = 20,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FilterStudentAsync("12345678");

			Assert.IsInstanceOf<SearchStudentViewModel>(result);
		}
		[Test]
		public async Task GetStudentDetailsByIdAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			var personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new ApplicationUser
			{
				Id = guid,
				UserName = "Test",
				Email = "Test"
			});
			await repository.AddAsync(new Student
			{
				Id = 20,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
				UserId = guid
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetStudentDetailsByIdAsync(20);

			Assert.NotNull(result);
			Assert.IsInstanceOf<StudentDetailsViewModel>(result);
			Assert.AreEqual(20, result.Id);
			Assert.AreEqual("Test", result.FirstName);
		}

		[Test]
		public async Task GetSubjects_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test",
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10,
			});

			var result = await personalInfoService.GetSubjects(guid);

			Assert.IsInstanceOf<IEnumerable<SubjectIndexViewModel>>(result);
			Assert.IsNotNull(result);
		}
		[Test]
		public async Task RemoveBookRentAsync_ShouldRemoveRent()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new Book
			{
				Id = 10,
				Title = "Test",
				Description = "Test",
				RenterId = guid,
				IsRented = true,
				RentalDate = DateTime.Now,
				LibraryId = 1
			});
			await repository.SaveChangesAsync();

			await personalInfoService.RemoveBookRentAsync(10, guid);

			var result = await repository.GetById<Book>(10);
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsRented);
			Assert.AreEqual(null, result.RenterId);
			Assert.AreEqual(null, result.RentalDate);
		}
		[Test]
		public async Task RentedBookExistsByIdAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Book
			{
				Id = 10,
				Title = "Test",
				Description = "Test",
				RenterId = "Test",
				IsRented = true,
				RentalDate = DateTime.Now,
				LibraryId = 1
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.RentedBookExistsByIdAsync(10);

			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task RentedBookExistsByIdAsync_ShouldReturnFalse()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Book
			{
				Id = 10,
				LibraryId = 1,
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.RentedBookExistsByIdAsync(11);
			Assert.IsFalse(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task StudentWithIdExistsAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.StudentWithIdExistsAsync(10);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task StudentWithIdExistsAsync_ShouldReturnFalse()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.StudentWithIdExistsAsync(11);
			Assert.IsFalse(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task UserWithIdExistsAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
				UserId = guid
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.UserWithIdExistsAsync(guid);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task UserWithIdExistsAsync_ShouldReturnFalse()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new Student
			{
				Id = 10,
				UserId = guid
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.UserWithIdExistsAsync("Test");

			Assert.IsFalse(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task GetEditSubjectFormAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new Student
			{
				Id = 20,
				UserId = guid,
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10
			});

			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetEditSubjectFormAsync(10, 20);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<EditSubjectFormViewModel>(result);
		}
		[Test]
		public async Task EditSubjectAsync_ShouldEditSubject()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new Student
			{
				Id = 20,
				UserId = guid,
				MajorId = 1,
				FacultyNumber = "12345678",
				FacultyId = 1,
				CourseTermId = 1,
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectProfessor
			{
				Id = 10,
				Title = "Professor"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectProfessor
			{
				Id = 11,
				Title = "Assistant"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectByProfessor
			{
				ProfessorId = 10,
				SubjectId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectByProfessor
			{
				ProfessorId = 11,
				SubjectId = 10
			});
			await repository.SaveChangesAsync();

			var model = new EditSubjectFormViewModel
			{
				Name = "Update",
				Description = "Update",
				TotalAttendanceCount = 10,
				SubjectProfessor = new SubjectProfessorIndexViewModel
				{
					Id = 10,
					Title = "Test",
				},
				SubjectAssistant = new SubjectAssistantIndexViewModel
				{
					Id = 11,
					Title = "Test",
				}
			};
			await personalInfoService.EditSubjectAsync(10, model);

			var result = await repository.GetById<Subject>(10);

			Assert.IsNotNull(result);
			Assert.AreEqual("Update", result.Name);
			Assert.AreEqual("Update", result.Description);
			Assert.AreEqual(10, result.TotlaAttendanceCount);
		}

		[Test]
		public async Task FilterFacultiesAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FilterFacultiesAsync(null,1, 2);

			Assert.IsInstanceOf<AllFacultiesViewModel>(result);
			Assert.That(result.Faculties.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task FilterFacultiesAsync_ShouldReturnModelWithFaculty()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

	        var result = await personalInfoService.FilterFacultiesAsync("Test", 1, 2);

			Assert.IsInstanceOf<AllFacultiesViewModel>(result);
			Assert.That(result.Faculties.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task AddFacultyAsync_ShouldAddFaculty()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var model = new FacultyFormViewModel
			{
				Name = "Test",
				Description = "Test"
			};
			await personalInfoService.AddFacultyAsync(model);

			var result = await repository.All<Faculty>().FirstOrDefaultAsync(x => x.Name == "Test");

			Assert.IsNotNull(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual("Test", result.Description);

		}
		[Test]
		public async Task FacultyExistsByIdAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FacultyExistsByIdAsync(10);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task FacultyExistsByIdAsync_ShouldReturnFalse()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FacultyExistsByIdAsync(11);
			Assert.IsFalse(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task GetMajorsForFacultyAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
                Id = 10
			});	

			await repository.SaveChangesAsync();
			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetMajorsForFacultyAsync(10);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<IEnumerable<MajorIndexViewModel>>(result);
			Assert.That(result.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task GetEditFacultyFormAsync_ShouldReturnModel()
		{
			var	repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();
			
			var result = await personalInfoService.GetEditFacultyFormAsync(10);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<FacultyFormViewModel>(result);
		}
		[Test]
		public async Task EditFacultyAsync_ShouldEditFaculty()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var model = new FacultyFormViewModel
			{
				Name = "Update",
				Description = "Update"
			};
			await personalInfoService.EditFacultyAsync(10, model);

			var result = await repository.GetById<Faculty>(10);
			Assert.IsNotNull(result);
			Assert.AreEqual("Update", result.Name);
			Assert.AreEqual("Update", result.Description);
		}
		[Test]
		public async Task FilterMajorsAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
			});

			await repository.SaveChangesAsync();
			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FilterMajorsAsync(null, 1, 2);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<AllMajorsViewModel>(result);
			Assert.That(result.Majors.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task FilterMajorsAsync_ShouldReturnModelWithMajor()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
			});

			await repository.SaveChangesAsync();
			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.FilterMajorsAsync("Test", 1, 2);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<AllMajorsViewModel>(result);
			Assert.That(result.Majors.Any(x => x.Id == 10), Is.True);
			Assert.AreEqual(1, result.CurrentPage);
			Assert.AreEqual(4, result.MajorsPerPage);
		}
		[Test]
		public async Task MajorExistsByIdAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();
			var result = await personalInfoService.MajorExistsByIdAsync(10);	
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task GetEditMajorFormAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10,
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetEditMajorFormAsync(10);
			Assert.IsNotNull(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual("Test", result.Description);
			Assert.IsInstanceOf<MajorFormViewModel>(result);
		}
		[Test]
		public async Task EditMajorAsync_ShouldEditMajor()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10,
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			var model = new MajorFormViewModel
			{
				Name = "Update",
				Description = "Update",
			};
			await personalInfoService.EditMajorAsync(10, model);

			var result = await repository.GetById<Major>(10);
			Assert.IsNotNull(result);
			Assert.AreEqual("Update", result.Name);
			Assert.AreEqual("Update", result.Description);
			Assert.AreEqual(10, result.FacultyId);
		}
		[Test]
		public async Task AddMajorAsync_ShouldAddMajor()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test"
			});
			await repository.SaveChangesAsync();

			var model = new MajorFormViewModel
			{
				Name = "Test",
				FacultyName = "Test"
			};
			await personalInfoService.AddMajorAsync(model);
			
			var result = await repository.All<Major>().FirstOrDefaultAsync(x => x.Name == "Test");
			Assert.IsNotNull(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual(10, result.FacultyId);
		}
		[Test]
		public async Task GetAttendanceRecordForStudentAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			var personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty 
			{
				Id = 10, 
				Name = "Test" 
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major 
			{
				Id = 10, 
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new CourseTerm 
			{
				Id = 10, 
				Name = "Test",
				MajorId = 10 
			});
			await repository.SaveChangesAsync();

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 10, 
				FacultyNumber = "12345600",
				FacultyId = 10, 
				CourseTermId = 10, 
				UserId = guid
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject 
			{ 
				Id = 10, 
				Name = "Test", 
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent 
			{ 
				StudentId = guid, 
				SubjectId = 10 
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetAttendanceRecordForStudentAsync(10, 10);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<ManageAttendanceViewModel>(result);
		}

		[Test]
		public async Task AddAttendanceAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			var personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new CourseTerm
			{
				Id = 10,
				Name = "Test",
				MajorId = 10
			});
			await repository.SaveChangesAsync();

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 10,
				FacultyNumber = "12345600",
				FacultyId = 10,
				CourseTermId = 10,
				UserId = guid
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test",
				TotlaAttendanceCount = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10,
				AttendanceRecord = 5
			});
			await repository.SaveChangesAsync();

			await personalInfoService.AddAttendanceAsync(10,10);

			var result = await repository.All<SubjectForStudent>().FirstOrDefaultAsync(x => x.StudentId == guid && x.SubjectId == 10);
			Assert.IsNotNull(result);
			Assert.AreEqual(6, result.AttendanceRecord);
			Assert.AreEqual(10, result.Subject.TotlaAttendanceCount);
			Assert.AreEqual(10, result.SubjectId);
		}
		[Test]
		public async Task UserWithIdHasRentedBookAsync_ShouldReturnTrue()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
				Id = guid,
			});
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Book
			{
				Id = 10,
				Title = "Test",
				RenterId = guid,
				IsRented = true,
				LibraryId = 1
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.UserWithIdHasRentedBookAsync(10, guid);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
			
		}
		[Test]
		public async Task LoadPersonalInfoAsync_ShouldReturnModel()
		{
			var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
				Id = guid,
				UserName = "Test",
				Email = "Test"
			});
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new CourseTerm
			{
				Id = 10,
				Name = "Test",
				MajorId = 10
			});
			await repository.SaveChangesAsync();

			
			await repository.AddAsync(new Student
			{
				Id = 10,
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				MajorId = 10,
				FacultyNumber = "12345678",
				FacultyId = 10,
				CourseTermId = 10,
				UserId = guid
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10
			});

			var result = await personalInfoService.LoadPersonalInfoAsync(guid);
			Assert.NotNull(result);
			Assert.IsInstanceOf<PersonalInfoViewModel>(result);
			Assert.That(result.FacultyNumber == "12345678", Is.True);
		}
		[Test]
		public async Task MyRentedBooksAsync_ShouldReturnModel()
		{
		      var repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });

			await repository.SaveChangesAsync();
			
			await repository.AddAsync(new BookCategory
			{
                Id = 10,
                Name = "Test"
            });

			await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 10,
                Title = "Test",
                RenterId = guid,
                IsRented = true,
                LibraryId = 1,
				CategoryId = 10
            });
            await repository.SaveChangesAsync();

			var result = await personalInfoService.MyRentedBooksAsync(guid, 1, 3);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<MyRentedBooksViewModel>(result);
			Assert.That(result.Books.Any(x => x.Id == 10), Is.True);

        }
		[Test]
		public async Task UserHasJoinedEventWithIdAsync_ShouldReturnTrue()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Event
			{
                Id = 10,
                Name = "Test",
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new EventParticipant
			{
                ParticipantId = guid,
                EventId = 10
            });
			await repository.SaveChangesAsync();

			var result = await personalInfoService.UserHasJoinedEventWithIdAsync(guid);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task JoinedEventsAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Event
			{
                Id = 10,
                Name = "Test",
            });
			await repository.SaveChangesAsync();
		    await repository.AddAsync(new EventParticipant
				{
                ParticipantId = guid,
                EventId = 10
            });
			await repository.SaveChangesAsync();

			var result = await personalInfoService.JoinedEventsAsync(guid, 1, 3);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<MyJoinedEventsViewModel>(result);
			
			Assert.That(result.Events.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task RemoveJoinAsync_ShouldRemoveEentParticipant()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Event
			{
                Id = 10,
                Name = "Test",
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new EventParticipant
			{
                ParticipantId = guid,
                EventId = 10
            });

			await repository.SaveChangesAsync();
			await personalInfoService.RemoveJoinAsync(10, guid);

			var result = await repository.All<EventParticipant>().FirstOrDefaultAsync(x => x.ParticipantId == guid && x.EventId == 10);
			Assert.IsNull(result);

		}
		[Test]
		public async Task MyRentedRoomsAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new StudyRoom
			{
                Id = 10,
                Name = "Test",
				IsRented = true,
				RenterId = guid,
				LibraryId = 1
            });
           
			await repository.SaveChangesAsync();

			var result = await personalInfoService.MyRentedRoomsAsync(guid, 1, 3);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<MyRentedRoomsViewModel>(result);
			Assert.That(result.Rooms.Any(x => x.Id == 10), Is.True);

		}
		[Test]
		public async Task GetStudentIdByUserIdAsync_ShouldReturnId()
		{
			   repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);
			var guid = Guid.NewGuid().ToString();

			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.AddAsync(new Student
			{
                Id = 10,
                UserId = guid
            });

			await repository.SaveChangesAsync();
			var result = await personalInfoService.GetStudentIdByUserIdAsync(guid);

			Assert.AreEqual(10, result);

		}
		[Test]
		public async Task SeeMyAttendanceRecordAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Student
			{
                Id = 10,
                UserId = guid
            });

			await repository.SaveChangesAsync();
			await repository.AddAsync(new Subject
			{
                Id = 10,
                Name = "Test",
                Description = "Test",
				TotlaAttendanceCount = 10
            });
			await repository.SaveChangesAsync();
			await repository.AddAsync(new SubjectForStudent
			{
                StudentId = guid,
                SubjectId = 10,
				AttendanceRecord = 5
            });

			await repository.SaveChangesAsync();
			var result = await personalInfoService.SeeMyAttendanceRecordAsync( 10, guid);	

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<MyAttendanceViewModel>(result);
			Assert.AreEqual(5, result.StudentAttendanceRecord);
			Assert.AreEqual(10, result.SubjectTotalAttendance);
			Assert.AreEqual(5, result.RemainingAttendance);

		}
		[Test]
		public async Task SubjectWithIdExistsAsync_ShouldReturnTrue()
		{
			   repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Subject
			{
                Id = 10,
                Name = "Test",
                Description = "Test"
            });
			await repository.SaveChangesAsync();

			var result = await personalInfoService.SubjectWithIdExistsAsync(10);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task SeeMySubjectDetailsAsync_ShouldReturnModel()
		{
			repository= new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
                Id = guid,
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Faculty
			{
                Id = 10,
                Name = "Test"
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
                Id = 10,
                Name = "Test",
                FacultyId = 10
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new CourseTerm
			{
                Id = 10,
                Name = "Test",
                MajorId = 10
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Student
			{
                Id = 10,
                FirstName = "Test",
                MajorId = 10,
                FacultyNumber = "12345678",
                FacultyId = 10,
                CourseTermId = 10,
                UserId = guid
            });

			await repository.SaveChangesAsync();

	        await repository.AddAsync(new Subject
				{
                Id = 10,
                Name = "Test",
                Description = "Test"
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
                StudentId = guid,
                SubjectId = 10
            });

			await repository.SaveChangesAsync();
			await repository.AddAsync(new SubjectProfessor
			{
                Id = 10,
                Title = "Professor"
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectProfessor
			{
                Id = 11,
                Title = "Assistant"
            });

			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectByProfessor
			{
                ProfessorId = 10,
                SubjectId = 10
            });

			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectByProfessor
			{
                ProfessorId = 11,
                SubjectId = 10
            });

			await repository.SaveChangesAsync();

			var result = await personalInfoService.SeeMySubjectDetailsAsync(10, guid);

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<SeeMySubjectDetailsViewModel>(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual("Professor", result.Professor.Title);
		}
        [Test]
        public async Task GetProfessorForSubjectAsync_ShouldReturnModel()
        {
            repository = new Repository(context);
            personalInfoService = new PersonalInfoService(repository);

            await repository.AddAsync(new SubjectProfessor
            {
                Id = 10,
                Title = "Professor",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "0888888888",
            });
            await repository.SaveChangesAsync();

            await repository.AddAsync(new Subject
            {
                Id = 10,
            });
            await repository.SaveChangesAsync();

            await repository.AddAsync(new SubjectByProfessor
            {
                ProfessorId = 10,
                SubjectId = 10
            });
            await repository.SaveChangesAsync();

            var result = await personalInfoService.GetProfessorForSubjectAsync(10);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProfessorDetailsViewModel>(result);
            Assert.AreEqual("Professor", result.Title); 
            Assert.AreEqual("Test", result.FirstName);
        }
		[Test]
		public async Task GetAssistantForSubjectAsync_ShouldReturnModel()
		{
			   repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new SubjectProfessor
			{
                Id = 10,
                Title = "Assistant",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "0888888888",
            });
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject
			{
                Id = 10,
            });
			await repository.SaveChangesAsync();
		    
			await repository.AddAsync(new SubjectByProfessor
			{
                ProfessorId = 10,
                SubjectId = 10
            });
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetAssistantForSubjectAsync(10);

			Assert.IsNotNull(result);
			Assert.IsInstanceOf<AssistantDetailsViewModel>(result);
			Assert.AreEqual("Assistant", result.Title);
			Assert.AreEqual("Test", result.FirstName);
		}
		[Test]
		public async Task StudentHasSubjectAsync_ShouldReturnTrue()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			var guid = Guid.NewGuid().ToString();
			await repository.AddAsync(new ApplicationUser
			{
				Id = guid,
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Student
			{
				Id = 10,
				UserId = guid
			});
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = guid,
				SubjectId = 10
			});

			await repository.SaveChangesAsync();

			var result = await personalInfoService.StudentHasSubjectAsync(10, 10);
			Assert.IsTrue(result);
			Assert.IsInstanceOf<bool>(result);
		}
		[Test]
		public async Task GetFacultyDetailsAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();
			var result = await personalInfoService.GetFacultyDetailsAsync(10);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<FacultyDetailsViewModel>(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual("Test", result.Description);

		}
		[Test]
		public async Task GetMajorsForFacultyByIdAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetMajorsForFacultyByIdAsync(10);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<IEnumerable<MajorDetailsViewModel>>(result);
			Assert.That(result.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task GetMajorDetailsAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				Description = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetMajorDetailsAsync(10);
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<MajorDetailsViewModel>(result);
			Assert.AreEqual("Test", result.Name);
			Assert.AreEqual("Test", result.Description);
		}
		[Test]
		public async Task GetSubjectsForMajorAsync_ShouldReturnModel()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test",
				MajorId = 10
			});
			await repository.SaveChangesAsync();

			var result = await personalInfoService.GetSubjectsForMajorAsync(10);
			Assert.IsNotNull(result);

			Assert.IsInstanceOf<IEnumerable<SubjectIndexViewModel>>(result);
			Assert.That(result.Any(x => x.Id == 10), Is.True);
		}
		[Test]
		public async Task DeleteFacultyAsync_ShouldDelete()
		{
			repository = new Repository(context);
			personalInfoService = new PersonalInfoService(repository);

			await repository.AddAsync(new Faculty
			{
				Id = 10,
				Name = "Test",
				Description = "Test"
			});
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Major
			{
				Id = 10,
				Name = "Test",
				FacultyId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new CourseTerm
			{
				Id = 10,
				Name = "Test",
				MajorId = 10
			});

			await repository.SaveChangesAsync();

			await repository.AddAsync(new Subject
			{
				Id = 10,
				Name = "Test",
				Description = "Test",
				MajorId = 10
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new Student
			{
				Id = 10,
				MajorId = 10,
				FacultyNumber = "12345678",
				FacultyId = 10,
				CourseTermId = 10,
			});
			await repository.SaveChangesAsync();

			await repository.AddAsync(new SubjectForStudent
			{
				StudentId = "Test",
				SubjectId = 10
			});	
			await repository.SaveChangesAsync();

			await personalInfoService.DeleteFacultyAsync(10);
			var faculty = await repository.GetById<Faculty>(10);
			var major = await repository.GetById<Major>(10);
			var subject = await repository.GetById<Subject>(10);
			var subjectForStudent = await repository.All<SubjectForStudent>().FirstOrDefaultAsync(x => x.SubjectId == 10);

			Assert.IsNull(faculty);
			Assert.IsNull(major);
			Assert.IsNull(subject);
			Assert.IsNull(subjectForStudent);
		}
		[TearDown]
		public void TearDown()
		{
			context.Database.EnsureDeleted();
		}
	}
}
