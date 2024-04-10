using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        [Test]
        public async Task AddNewStudentAsync_ShouldAddNewStudent()
        {
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);


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
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);

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
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);

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
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);

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
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);

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
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);


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
    }
}
