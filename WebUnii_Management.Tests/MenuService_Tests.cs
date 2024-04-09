using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Menu;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUnii_Management.Tests
{
    [TestFixture]
    public class MenuService_Tests
    {
        private IMenuService menuService;
        private IRepository repository;
        private ApplicationDbContext context;
        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MenuService_Tests")
                .Options;

            context = new ApplicationDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task GetMenuAsync_ShouldReturnModel()
        {
            repository = new Repository(context);   
            menuService = new MenuService(repository);

            await repository.AddAsync(new Dish
            {
                Id = 7,
                Name = "TestMenu",
                Category = "TestCategory",
                Price = 10.00m,
            });
            await repository.SaveChangesAsync();

            await repository.SaveChangesAsync();

            var result = await menuService.GetMenuAsync();
            var dish = await repository.GetById<Dish>(7);

            Assert.AreEqual(1, result.Id);
            Assert.IsInstanceOf<MenuIndexViewModeltest>(result);
            Assert.AreEqual(7, dish.Id);
            Assert.AreEqual("TestMenu", dish.Name);
        }
    }
}
