using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Library;
using WebUni_Management.Core.Services;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;


namespace WebUnii_Management.Tests
{
    [TestFixture]
    public class LibraryService_Tests
    {
        private ILibraryService libraryService;
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

			repository = new Repository(context);
			libraryService = new LibraryService(repository);
		}
        [Test]
        public async Task AllBooksAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new Book
            {
                Id = 10,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 11,
                Title = "Test2",
                PublishYear = "2",
                Description = "Test2",
                ImageUrl = "test2",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 12,
                Title = "Test3",
                PublishYear = "3",
                Description = "Test3",
                ImageUrl = "test3",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.AllBooksAsync(null, null, 1, 3);
            var book = await repository.GetById<Book>(10);
            
            Assert.AreEqual(8, result.TotalBooksCount);
            Assert.AreEqual(3, result.BooksPerPage);
            Assert.IsInstanceOf<AllBooksQueryModel>(result);
            Assert.AreEqual("Test", book.Title);
            Assert.That(result.Books.Any(x => x.Id == 13), Is.False);
        }

        [Test]
        public async Task AllBooksAsync_ShouldReturnModelWithSearchTerm()
        {
            await repository.AddAsync(new Book
            {
                Id = 10,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 11,
                Title = "Test2",
                PublishYear = "2",
                Description = "Test2",
                ImageUrl = "test2",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 12,
                Title = "Test3",
                PublishYear = "3",
                Description = "Test3",
                ImageUrl = "test3",
                LibraryId = 1
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.AllBooksAsync(null, "Test", 1, 3);
            var book = await repository.GetById<Book>(10);

            Assert.AreEqual(3, result.BooksPerPage);
            Assert.IsInstanceOf<AllBooksQueryModel>(result);
            Assert.AreEqual("Test", book.Title);
        }

        [Test]
        public async Task AllCategorisNamesAsync_ShouldReturnStringWithCategories()
        {
            await repository.AddAsync(new BookCategory
            {
                Id = 4,
                Name = "Test"
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new BookCategory
            {
                Id = 5,
                Name = "Test2"
            });

            var result = await libraryService.AllCategorisNamesAsync();     
            var category = await repository.GetById<BookCategory>(4);

            Assert.AreEqual("Test", category.Name);
            Assert.AreEqual(4, result.Count()); 
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task BookDetailsAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new BookAuthor
            {
				Id = 9,
				FirstName = "Test",
				LastName = "Test",
			});
            await repository.SaveChangesAsync();
			await repository.AddAsync(new BookCategory
			{
				Id = 4,
				Name = "Test"
			});
            await repository.SaveChangesAsync();
			await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                CategoryId = 4
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new BookByBookAuthor
            {
				BookId = 13,
				AuthorId = 9
			});
            await repository.SaveChangesAsync();

            var result = await libraryService.BookDetailsAsync(13);           

            Assert.AreEqual(13, result.Id);
            Assert.IsInstanceOf<BookDetailsViewModel>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
        }

        [Test]
        public async Task BookExistsByIdAsync_ShouldReturnTrue()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.BookExistsByIdAsync(13);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task GetEditFormBookModelAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.GetEditFormBookModelAsync(13);

            Assert.IsInstanceOf<EditBookViewModel>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
        }

        [Test]
        public async Task AllCategoriesForEditAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new BookCategory
            {
                Id = 4,
                Name = "Test"
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new BookCategory
            {
                Id = 5,
                Name = "Test2"
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.AllCategoriesForEditAsync();

            Assert.IsInstanceOf<IEnumerable<BookCategoryViewModel>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public async Task IsBookRentedAsync_ShouldReturnTrue()
        { 
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = true

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.IsBookRentedAsync(13);    

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task IsBookRentedAsync_ShouldReturnFalse()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.IsBookRentedAsync(13);

            Assert.IsFalse(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task LastThreeBooksAsync_ShoudReturnModel()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false

            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 14,
                Title = "Test2",
                PublishYear = "2",
                Description = "Test2",
                ImageUrl = "test2",
                LibraryId = 1,
                IsRented = false

            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 15,
                Title = "Test3",
                PublishYear = "3",
                Description = "Test3",
                ImageUrl = "test3",
                LibraryId = 1,
                IsRented = false
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.LastThreeBooksAsync();

            Assert.IsInstanceOf<IEnumerable<BookInfoViewModel>>(result);
            Assert.That(result.Any(x => x.Id == 1), Is.False);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAuthor_ShouldReturnString()
        {
            await repository.AddAsync(new BookAuthor
            {
                Id = 9,
                FirstName = "Test Test",
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new BookByBookAuthor
            {
                BookId = 13,
                AuthorId = 9
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.GetAuthor(13);

            Assert.AreEqual("Test Test", result);
            Assert.IsInstanceOf<string>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task LastThreeStudyRoomsAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new StudyRoom
            {
                Id = 14,
                Name = "Test2",
                Description = "Test2",
                ImageUrl = "test2",
                LibraryId = 1,
                IsRented = false,
                Capacity = 12
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new StudyRoom
            {
                Id = 15,
                Name = "Test3",
                Description = "Test3",
                ImageUrl = "test3",
                LibraryId = 1,
                IsRented = false,
                Capacity = 13
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.LastThreeStudyRoomsAsync();

            Assert.IsInstanceOf<IEnumerable<StudyRoomInfo>>(result);
            Assert.That(result.Any(x => x.Capacity == 1), Is.False);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task RentBookAsync_ShouldChangeBookRent()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false
            });
            await repository.SaveChangesAsync();
            var guid = Guid.NewGuid().ToString();

            await libraryService.RentBookAsync(13, guid);
            var book = await repository.GetById<Book>(13);

            Assert.IsTrue(book.IsRented);
            Assert.That(book.RenterId, Is.Not.Null);
            Assert.AreEqual(guid, book.RenterId);
            Assert.AreEqual(13, book.Id);
        }

        [Test]
        public async Task CategoryExistsById_ShouldReturntrue()
        {
            await repository.AddAsync(new BookCategory
            {
                Id = 13,
                Name = "Test",
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.CategoryExistsById(13);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task CategoryExistsById_ShouldReturnFalse()
        {
            await repository.AddAsync(new BookCategory
            {
                Id = 13,
                Name = "Test",
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.CategoryExistsById(14);

            Assert.IsFalse(result);
            Assert.IsInstanceOf<bool>(result);
        }

		[Test]
		public async Task EditBookAsync_ShouldReturnModel()
		{
			await repository.AddAsync(new BookAuthor
			{
				Id = 9,
				FirstName = "Test",
				LastName = "Test",
			});
			await repository.SaveChangesAsync();
			await repository.AddAsync(new Book
			{
				Id = 13,
				Title = "Test",
				PublishYear = "1",
				Description = "Test",
				ImageUrl = "test",
				LibraryId = 1,
			});
			await repository.SaveChangesAsync();
            await repository.AddAsync(new BookByBookAuthor
            {
				BookId = 13,
				AuthorId = 9
			}); 
            await repository.SaveChangesAsync();
			var model = new EditBookViewModel
			{
				Id = 13,
				Title = "Test2",
				PublishYear = "2",
				Description = "Test2",
				ImageUrl = "test2",
				Author = "Test Author", 
				CategoryId = 1 
			};

			await libraryService.EditBookAsync(13, model);
			var book = await repository.GetById<Book>(13);
			var author = await repository.All<BookByBookAuthor>().Where(x => x.BookId == 13).Select(x => x.Author).FirstOrDefaultAsync();

			Assert.AreEqual("Test2", book.Title);
			Assert.AreEqual("2", book.PublishYear);
			Assert.AreEqual("Test2", book.Description);
			Assert.AreEqual("test2", book.ImageUrl);
			Assert.AreEqual(13, book.Id);
			Assert.IsFalse(book.IsRented);
			Assert.IsInstanceOf<Book>(book);
			Assert.AreEqual("Test", author.FirstName);
		}

		[Test]
        public async Task AllRoomsAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new StudyRoom
            {
                Id = 14,
                Name = "Test2",
                Description = "Test2",
                ImageUrl = "test2",
                LibraryId = 1,
                IsRented = false,
                Capacity = 12
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new StudyRoom
            {
                Id = 15,
                Name = "Test3",
                Description = "Test3",
                ImageUrl = "test3",
                LibraryId = 1,
                IsRented = false,
                Capacity = 13
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.AllRoomsAsync(null, null, 1, 3);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.NotNull(room);
            Assert.AreEqual(3, result.RoomsPerPage);
            Assert.IsInstanceOf<AllRoomsQueryModel>(result);
            Assert.That(result.StudyRooms.Any(x => x.Id == 16), Is.False);
        }

        [Test]
        public async Task RoomExistsByIdAsync_ShouldReturnTrue()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.RoomExistsByIdAsync(13);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task RoomExistsByIdAsync_ShouldReturnFalse()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.RoomExistsByIdAsync(14);

            Assert.IsFalse(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task GetRoomDetailsByIdAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.GetRoomDetailsByIdAsync(13);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsNotNull(room);
            Assert.IsInstanceOf<RoomShowcaseViewModel>(result);
        }

        [Test]
        public async Task GetEditRoomAsync_ShouldReturnModel()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11

            });
            await repository.SaveChangesAsync();

            var result = await libraryService.GetEditRoomAsync(13);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsNotNull(room);
            Assert.IsInstanceOf<EditRoomViewModel>(result);
            Assert.AreEqual("Test", room.Name);
            Assert.AreEqual("Test", room.Description);
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBook()
        {
            var model = new EditBookViewModel
            {
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",          
                Author = "Test Test",
            };

            await libraryService.AddBookAsync(model);
            var book = await repository.All<Book>().FirstOrDefaultAsync(x => x.Title == "Test");

            Assert.IsNotNull(book);
            Assert.AreEqual("Test", book.Title);
        }

        [Test]
        public async Task AddRoomAsync_ShouldAddRoom()
        {
            var model = new EditRoomViewModel
            {
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                Capacity = 11
            };

            await libraryService.AddRoomAsync(model);
            var room = await repository.All<StudyRoom>().FirstOrDefaultAsync(x => x.Name == "Test");

            Assert.IsNotNull(room);
            Assert.AreEqual("Test", room.Name);
        }

        [Test]
        public async Task ManageBookRentAsync_ShouldManageBookRent()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
                Id = guid
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new Book
            {
				Id = 14,
				Title = "Test",
				PublishYear = "1",
				Description = "Test",
				ImageUrl = "test",
				LibraryId = 1,
				IsRented = true,
				RenterId = guid,
                RentalDate = DateTime.Now.AddDays(-68)
			});

            var result = await libraryService.ManageBookRentAsync();
            var book = await repository.GetById<Book>(14);

            Assert.IsInstanceOf<ManageRentViewModel>(result);
            Assert.IsNotNull(result);
            Assert.IsFalse(book.IsRented);
        }

        [Test]
        public async Task DeleteBookAsync_ShouldDeleteBookAsync()
        {
            await repository.AddAsync(new Book
            {
                Id = 13,
                Title = "Test",
                PublishYear = "1",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false
            });
            await repository.SaveChangesAsync();
            await repository.AddAsync(new BookByBookAuthor
            {
                BookId = 13,
                AuthorId = 1
            });
            await repository.SaveChangesAsync();

            await libraryService.DeleteBookAsync(13);   
            var book = await repository.GetById<Book>(13);
            var bookAuthor = await repository.All<BookByBookAuthor>().FirstOrDefaultAsync(x => x.BookId == 13);

            Assert.IsNull(book);
            Assert.IsNull(bookAuthor); 
        }

        [Test]
        public async Task DeleteRoomAsync_ShouldDeleteRoom()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11
            });
            await repository.SaveChangesAsync();

            await libraryService.DeleteRoomAsync(13);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsNull(room);
        }

        [Test]
        public async Task EditRoomAsync_ShouldEditRoom()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11
            });
            await repository.SaveChangesAsync();
            var model = new EditRoomViewModel
            {
                Id = 13,
                Name = "Test2",
                Description = "Test2",
                ImageUrl = "test2",
                Capacity = 12
            };

            await libraryService.EditRoomAsync(13, model);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsNotNull(room);
            Assert.AreEqual("Test2", room.Name);
            Assert.AreEqual("Test2", room.Description);
            Assert.AreEqual("test2", room.ImageUrl);
            Assert.AreEqual(12, room.Capacity);
            Assert.AreEqual(13, room.Id);
        }

        [Test]
        public async Task IsRoomRentedAsync_ShouldReturnTrue()
        {
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = true,
                Capacity = 11
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.IsRoomRentedAsync(13);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task IsRoomRentedByUserWithIdAsync_ShouldReturnTrue()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = true,
                Capacity = 11,
                RenterId = guid
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.IsRoomRentedByUserWithIdAsync(guid, 13);

            Assert.IsTrue(result);
            Assert.IsInstanceOf<bool>(result);
        }

        [Test]
        public async Task RentRoomAsync_ShouldRentRoom()
        {
            var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                Name = "Test",
                Description = "Test",
                ImageUrl = "test",
                LibraryId = 1,
                IsRented = false,
                Capacity = 11
            });
            await repository.SaveChangesAsync();

            await libraryService.RentRoomAsync(guid, 13);
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsTrue(room.IsRented);
            Assert.AreEqual(guid, room.RenterId);
            Assert.AreEqual(13, room.Id);
        }

        [Test]
        public async Task ManageRoomRentAsync_ShouldManageRoomRent()
        {
			var guid = Guid.NewGuid().ToString();
            await repository.AddAsync(new ApplicationUser
            {
				Id = guid
			});
            await repository.SaveChangesAsync();
            await repository.AddAsync(new StudyRoom
            {
                Id = 13,
                LibraryId = 1,
                IsRented = true,
                RenterId = guid,
                RentalDate = DateTime.Now.AddDays(-1)
            });
            await repository.SaveChangesAsync();

            var result = await libraryService.ManageRoomRentAsync();
            var room = await repository.GetById<StudyRoom>(13);

            Assert.IsInstanceOf<ManageRentViewModel>(result);
            Assert.IsNotNull(result);
            Assert.IsFalse(room.IsRented);
            Assert.IsNull(room.RenterId);
        }

		[TearDown]
		public void TearDown()
		{
			context.Database.EnsureDeleted();
		}
	}
}
