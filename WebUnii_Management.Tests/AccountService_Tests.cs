using Microsoft.EntityFrameworkCore;
using System.Text;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Account;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUnii_Management.Tests
{
	[TestFixture]
    public class AccountService_Tests
    {
        private IRepository repository;
        private IAccountService accountService;
        private ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

			repository = new Repository(dbContext);
			accountService = new AccountService(repository);
		}

        [Test]
        public async Task AddNewStudentAsync_ShouldAddNewStudent()
        {
            await repository.AddAsync(new Faculty
            {
                Name = "Test",
                Id = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Major 
            { 
                Name = "Test",
                Id = 10,
                FacultyId = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new CourseTerm
            {
                Name = "Test",
                Id = 10,
                MajorId = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                Email = "Test",
                IsApproved = true
            });
            await repository.SaveChangesAsync();
            var model = new ManageAccountViewModel
            {
                FirstName = "Test",
                LastName = "Test",
                Age = 20,
                PhoneNumber = "0888888888",
                Major = "Test",
                Faculty = "Test",
                CourseTerm = "Test"
            };

            await accountService.AddNewStudentAsync("Test", model);
            var student = await repository.All<Student>().FirstOrDefaultAsync(x => x.PhoneNumber == "0888888888");

            Assert.AreEqual("Test", student.FirstName);
            Assert.AreEqual("Test", student.LastName);
            Assert.AreEqual(20, student.Age);
            Assert.AreEqual("0888888888", student.PhoneNumber);
            Assert.AreEqual("Test", student.Major.Name);
            Assert.AreEqual("Test", student.Faculty.Name);
            Assert.AreEqual("Test", student.CourseTerm.Name);
        }

        [Test]
        public async Task FindUserByIdAsync_ShouldReturnUser()
        { 
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = guid,
            });
            await repository.SaveChangesAsync();

            var user = await accountService.FindUserByIdAsync(guid);

            Assert.AreEqual(guid, user.Id);
            Assert.IsTrue(user != null);
        }

        [Test]
        public async Task FillManageAccountAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new Faculty
            {
                Name = "Test",
                Id = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Major
            {
                Name = "Test",
                Id = 10,
                FacultyId = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new CourseTerm
            {
                Name = "Test",
                Id = 10,
                MajorId = 10
            });
            await repository.SaveChangesAsync();
            var userId = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                Email = "test", 
                IsApproved = true
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
                FacultyId = 10,
                CourseTermId = 10,
                UserId = "1",
            });
            await repository.SaveChangesAsync();

            var result = await accountService.FillManageAccountAsync("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Id);
            Assert.AreEqual("Test", result.FirstName);
            Assert.AreEqual("Test", result.LastName);
            Assert.AreEqual(20, result.Age);
            Assert.AreEqual("0888888888", result.PhoneNumber);
        }

        [Test]
        public async Task GetAllRequestsAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                Email = "test",
                IsApproved = false
            });
            await repository.SaveChangesAsync();

            var result = await accountService.GetAllRequestsAsync(1, 2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TotalRequests);
            Assert.IsInstanceOf<AllRequestsViewModel>(result);
            Assert.AreEqual(10, result.RequestsPerPage);
        }

        [Test]
        public async Task GetStudentAsync_ShouldReturnTrue()
        {
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

            var result = await accountService.GetStudentAsync(guid);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task GetUserByUserNameAsync_ShouldReturnUser()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = guid,
                UserName = "Test",
            });
            await repository.SaveChangesAsync();

            var result = await accountService.GetUserByUserNameAsync("Test");

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.UserName);
            Assert.IsInstanceOf<ApplicationUser>(result);
        }

        [Test]
        public async Task StudentExistsByIdAsync_ShouldReturnTrue()
        {
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

            var result = await accountService.StudentExistsByIdAsync(10);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task GetEditAccountFormAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new Faculty
            {
                Name = "Test",
                Id = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Major
            {
                Name = "Test",
                Id = 10,
                FacultyId = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new CourseTerm
            {
                Name = "Test",
                Id = 10,
                MajorId = 10
            });
            await repository.SaveChangesAsync();
            var userId = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                Email = "test",
                IsApproved = true
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
                FacultyId = 10,
                CourseTermId = 10,
                UserId = "1",
            });
            await repository.SaveChangesAsync();

            var result = await accountService.GetEditAccountFormAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Id);
            Assert.AreEqual("Test", result.FirstName);
            Assert.AreEqual("Test", result.LastName);
            Assert.AreEqual(20, result.Age);
            Assert.AreEqual("0888888888", result.PhoneNumber);
            Assert.AreEqual("Test", result.Major);
            Assert.AreEqual("Test", result.Faculty);
            Assert.AreEqual("Test", result.CourseTerm);
        }

        [Test]
        public async Task EditAccountAsync_ShouldEditAccount()
        {
            await repository.AddAsync(new Faculty
            {
                Name = "Test",
                Id = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Major
            {
                Name = "Test",
                Id = 10,
                FacultyId = 10
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new CourseTerm
            {
                Name = "Test",
                Id = 10,
                MajorId = 10
            });
            await repository.SaveChangesAsync();
            var userId = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = "1",
                UserName = "Test",
                Email = "test",
                IsApproved = true
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
                FacultyId = 10,
                CourseTermId = 10,
                UserId = "1",
            });
            await repository.SaveChangesAsync();
            var model = new ManageAccountViewModel
            {
                Id = 10,
                FirstName = "Test1",
                LastName = "Test1",
                Age = 21,
                PhoneNumber = "0888888889",
                Major = "Test",
                Faculty = "Test",
                CourseTerm = "Test"
            };

            await accountService.EditAccountAsync(10, model);
            var student = await repository.All<Student>().FirstOrDefaultAsync(x => x.PhoneNumber == "0888888889");

            Assert.AreEqual("Test1", student.FirstName);
            Assert.AreEqual("Test1", student.LastName);
            Assert.AreEqual(21, student.Age);
            Assert.AreEqual("0888888889", student.PhoneNumber);
            Assert.AreEqual("Test", student.Major.Name);
            Assert.AreEqual("Test", student.Faculty.Name);
            Assert.AreEqual("Test", student.CourseTerm.Name);
        }

        [Test]
        public async Task GetQrCodeForStudentAsync_ShouldReturnString()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = guid,
            });
            await repository.SaveChangesAsync();

            string qrCodeData = "someQrCode";
            byte[] qrCode = Encoding.UTF8.GetBytes(qrCodeData);

            await repository.AddAsync(new Student
            {
                Id = 10,
                UserId = guid,
                QRCode = qrCode
            });
            await repository.SaveChangesAsync();

            var result = await accountService.GetQrCodeForStudentAsync(guid);
            string base64Result = Convert.ToBase64String(qrCode);

            Assert.AreEqual(base64Result, result);
        }

        [Test]
        public async Task AddStudentAsync_ShouldAddStudent()
        {
            await repository.AddAsync(new Faculty
            {
				Name = "Test",
				Id = 10
			});
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Major
            {
                Name = "Test",
				Id = 10,
				FacultyId = 10
			});
            await repository.SaveChangesAsync();
            await repository.AddAsync(new CourseTerm
            {
                Name = "Test",
                Id = 10,
                MajorId = 10
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
				Id = guid,
				IsApproved = true
			});
            await repository.SaveChangesAsync();
            var model = new ManageAccountViewModel
            {
				FirstName = "Test",
				LastName = "Test",
				Age = 20,
				PhoneNumber = "0888888888",
				Major = "Test",
				Faculty = "Test",
				CourseTerm = "Test"
			};

            await accountService.AddStudentAsync(guid, model);
			var student = await repository.All<Student>().FirstOrDefaultAsync(x => x.PhoneNumber == "0888888888");

			Assert.AreEqual("Test", student.FirstName);
			Assert.AreEqual("Test", student.LastName);
			Assert.AreEqual(20, student.Age);
			Assert.AreEqual("0888888888", student.PhoneNumber);
			Assert.AreEqual("Test", student.Major.Name);
			Assert.AreEqual("Test", student.Faculty.Name);
			Assert.AreEqual("Test", student.CourseTerm.Name);
        }
		[TearDown]
		public void TearDown()
		{
			dbContext.Database.EnsureDeleted();
		}
	}
}
