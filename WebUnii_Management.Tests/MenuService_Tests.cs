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
        [Test]
        public async Task ChangeMenuDateAsync_ShouldChangeDate()
        {
            var repository = new Repository(context);   
            var menuService = new MenuService(repository);

            await repository.AddAsync(new Menu
            {
				Id = 2,
				Date = DateTime.Now.AddDays(-2).Date,
			});

            await repository.SaveChangesAsync();

			await menuService.ChangeMenuDateAsync(2);

			var menu = await repository.GetById<Menu>(2);

			Assert.AreEqual(DateTime.Now.Date, menu.Date.Date);
        }
        [Test]
        public async Task DishExistsById_ShouldReturnTrue()
        {
            var repository = new Repository(context);
            var menuService = new MenuService(repository);

            await repository.AddAsync(new Dish
            {
				Id = 10,
				Name = "TestDish",
				Category = "TestCategory",
				Price = 10.00m,
			});
            await repository.SaveChangesAsync();

            var result = await menuService.DishExistsById(10);
            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);

        }
        [Test]
        public async Task EditDishAsync_ShouldEditDish()
        {
			var repository = new Repository(context);
			var menuService = new MenuService(repository);

            await repository.AddAsync(new Dish
            {
                Id = 10,
                Name = "TestDish",
                Category = "TestCategory",
                Price = 1.00m,
            });
            await repository.SaveChangesAsync();

            var model = new DishFormViewModel
            {
				Name = "UpdateDish",
				Price = 1.00m
			};
            await menuService.EditDishAsync(10, model);

			var dish = await repository.GetById<Dish>(10);

			Assert.AreEqual("UpdateDish", dish.Name);
			Assert.AreEqual(1.00m, dish.Price);
        }
        [Test]
        public async Task GetDishForEditAsync_ShouldReturnModel()
        {
            var repository = new Repository(context);
            var menuService = new MenuService(repository);

            await repository.AddAsync(new Dish
            {
				Id = 10,
				Name = "TestDish",
				Category = "TestCategory",
				Price = 1.00m,
			});
            await repository.SaveChangesAsync();

			var result = await menuService.GetDishForEditAsync(10);

            Assert.IsInstanceOf<DishFormViewModel>(result);
			Assert.AreEqual("TestDish", result.Name);
			Assert.AreEqual(1.00m, result.Price);
        }
        [Test]
        public async Task MenuExistsById_ShouldReturnTrue()
        {
			var repository = new Repository(context);
			var menuService = new MenuService(repository);

            await repository.AddAsync(new Menu
            {
                Id = 10,
            });

            await repository.SaveChangesAsync();

            var result = await menuService.MenuExistsById(10);
            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

	}
}
