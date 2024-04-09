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
        public async Task AddNewStudentAsync_ShouldAddStudent()
        {
            repository = new Repository(dbContext);
            accountService = new AccountService(repository);

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
            await repository.AddAsync(new Major { Name = "Test" });
        }
    }
}
