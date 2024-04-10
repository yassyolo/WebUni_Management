using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Library;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IRepository repository;

        public LibraryService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<AllBooksQueryModel> AllBooksAsync(string? category = null, string? searchTerm = null, int currentPage = 1, int booksPerPage = 3)
        {
            var books = repository.AllReadOnly<Book>();

            if(category != null)
            {
                books = books.Where(x => x.Category.Name == category);
            }
            if(searchTerm != null)
            {
                var searchTermToLower = searchTerm.ToLower();
                books = books.Where(x => x.Title.Contains(searchTermToLower) || x.Author.Any(x => x.FirstName.Contains(searchTermToLower) || x.LastName.Contains(searchTermToLower)) || x.PublishYear.Contains(searchTermToLower));
            }
            var booksToShow = await books.Skip((currentPage - 1) * booksPerPage).Take(booksPerPage).OrderByDescending(x => x.Id)
                .Select(x => new BookShowcaseViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = string.Join(", ", x.Author.Select(x => x.FirstName + " " + x.LastName)),
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.Name,
                    Description = x.Description,
                    PublishYear = x.PublishYear,
                    IsRented = x.IsRented
                }).ToListAsync();
            foreach (var book in booksToShow)
            {
                await GetAuthor(book.Id);
            }
            return new AllBooksQueryModel
            {
                Books = booksToShow,
                TotalBooksCount = await books.CountAsync()
            };
        }

        public async Task<IEnumerable<string>> AllCategorisNamesAsync()
        {
            return await repository.AllReadOnly<BookCategory>().Select(x => x.Name).ToListAsync();        
        }

        public async Task<BookDetailsViewModel?> BookDetailsAsync(int id)
        {
            var author = await GetAuthor(id);
            return await repository.AllReadOnly<Book>()
                .Where(x => x.Id == id)
                .Select(x => new BookDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Authors = author,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.Name,
                    Description = x.Description,
                    PublishYear = x.PublishYear,
                    IsRented = x.IsRented
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> BookExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Book>().AnyAsync(x => x.Id == id);
        }

        public async Task<EditBookViewModel> GetEditFormBookModelAsync(int id)
        { 
            var author = await GetAuthor(id);
            var book = await repository.AllReadOnly<Book>()
                .Where(x => x.Id == id)
                .Select(x => new EditBookViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    PublishYear = x.PublishYear,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Author = author
                }).FirstOrDefaultAsync();
            if(book == null)
            {
                throw new InvalidOperationException("Book not found");
            }
            
            book.Categories = await AllCategoriesForEditAsync();
            return book;
        }

        public async Task<IEnumerable<BookCategoryViewModel>> AllCategoriesForEditAsync()
        {
            return await repository.AllReadOnly<BookCategory>()
                .Select(x => new BookCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<bool> IsBookRentedAsync(int id)
		{
			return await repository.AllReadOnly<Book>().Where(x => x.Id == id).Select(x => x.IsRented).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<BookInfoViewModel>> LastThreeBooksAsync()
        {
            
           var books= await repository.AllReadOnly<Book>()

                .OrderByDescending(x => x.Id)
                .Take(3)
                .Select(x => new BookInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                   
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl
                })
                
                .ToListAsync();
            foreach (var book in books)
            {
                book.Author = await GetAuthor(book.Id);
            }
            return books;
        }

        public async Task<string> GetAuthor(int id)
        {
            var authorViewModels = await repository.AllReadOnly<BookByBookAuthor>()
                .Where(x => x.BookId == id)
                .Select(x => new AuthorViewModel
                {
                    FirstName = x.Author.FirstName,
                    LastName = x.Author.LastName
                })
                .ToListAsync();
            return string.Join(", ", authorViewModels.Select(a => $"{a.FirstName} {a.LastName}")).TrimEnd();
        }

        public async Task<IEnumerable<StudyRoomInfo>> LastThreeStudyRoomsAsync()
        {
            return await repository.AllReadOnly<StudyRoom>()
                .OrderByDescending(x => x.Capacity)
                .Take(3)
                .Select(x => new StudyRoomInfo
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Capacity = x.Capacity,
                    IsRented = x.IsRented
                })
                .ToListAsync();
        }

        public async Task RentBookAsync(int id, string userId)
		{
			var book = await repository.GetById<Book>(id);
            if(book == null)
            {
				throw new InvalidOperationException("Book not found");
			}
            book.RenterId = userId;
            book.RentalDate= DateTime.Now;
            book.IsRented = true;
            await repository.SaveChangesAsync();
		}

        public async Task<bool> CategoryExistsById(int id)
        {
            return await repository.AllReadOnly<BookCategory>()
           .AnyAsync(x => x.Id == id);
        }

        public async Task EditBookAsync(int id, EditBookViewModel model)
        {
            var book = repository.All<Book>().FirstOrDefault(x => x.Id == id);
            if(book == null)
            {
                throw new InvalidOperationException("Book not found");
            }
            var authors = model.Author.Split(", ");
            foreach (var author in authors)
            {
                var names = author.Split(" ");
                var existingAuthor = await repository.All<BookAuthor>()
                    .Where(x => x.FirstName == names[0] && x.LastName == names[1])
                    .FirstOrDefaultAsync();
                if(existingAuthor == null)
                {
                    existingAuthor = new BookAuthor
                    {
                        FirstName = names[0],
                        LastName = names[1],
                    };
                    var bookBybokAuthor = new BookByBookAuthor
                    {
                        AuthorId = existingAuthor.Id,
                        BookId = book.Id
                    };
                    await repository.AddAsync(existingAuthor);
                }
            }
            book.Title = model.Title;
            book.ImageUrl = model.ImageUrl;
            book.PublishYear = model.PublishYear;
            book.Description = model.Description;
            book.CategoryId = model.CategoryId;
            await repository.SaveChangesAsync();
        }

        public async Task<AllRoomsQueryModel> AllRoomsAsync(int? capacity = null, string? searchTerm = null, int currentPage = 1, int roomsPerPage = 1)
        {
            var rooms = repository.AllReadOnly<StudyRoom>();

            if (capacity != 0 && capacity.HasValue)
            {
                rooms = rooms.Where(x => x.Capacity == capacity);
            }

            if (searchTerm != null && searchTerm != "")
            {
                var searchTermToLower = searchTerm.ToLower();
                rooms = rooms.Where(x => x.Name.Contains(searchTermToLower) || x.Description.Contains(searchTermToLower));
            }
            var roomsToShow = await rooms.Skip((currentPage - 1) * roomsPerPage).Take(roomsPerPage).OrderByDescending(x => x.Id)
                .Select(x => new RoomShowcaseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Capacity = x.Capacity,
                    IsRented = x.IsRented,
                    Floor = x.Floor,
                }).ToListAsync();
           return new AllRoomsQueryModel
            {
                StudyRooms = roomsToShow,
                TotalRooms = await rooms.CountAsync()
            };
        }

        public async Task<bool> RoomExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<StudyRoom>().AnyAsync(x => x.Id == id);
        }

        public async Task<RoomShowcaseViewModel?> GetRoomDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<StudyRoom>().Where(x => x.Id == id)
                .Select(x => new RoomShowcaseViewModel 
                { 
                    Id = x.Id, 
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Capacity = x.Capacity,
                    IsRented = x.IsRented,
                    Floor = x.Floor,
                }).FirstOrDefaultAsync();
        }

        public async Task<EditRoomViewModel?> GetEditRoomAsync(int id)
        {
            return await repository.AllReadOnly<StudyRoom>().Where(x => x.Id == id)
                .Select(x => new EditRoomViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Capacity = x.Capacity,
                    Floor = x.Floor,
                    IsRented = x.IsRented
                }).FirstOrDefaultAsync();
        }

        public async Task AddBookAsync(EditBookViewModel model)
        {
            var book = new Book
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,                
                CategoryId = model.CategoryId,
                PublishYear = model.PublishYear,
                Description = model.Description,
                LibraryId = 1
            };

            var authors = model.Author.Split(", ").ToArray();
            foreach (var author in authors)
            {
                var names = author.Split(" ").ToArray();
                var existingAuthor = await repository.All<BookAuthor>()
                    .Where(x => x.FirstName == names[0] || x.LastName == names[1])
                    .FirstOrDefaultAsync();
                if(existingAuthor == null)
                {
                    existingAuthor = new BookAuthor
                    {
                        FirstName = names[0],
                        LastName = names[1],
                    };
                    var bookBybokAuthor = new BookByBookAuthor
                    {
                        AuthorId = existingAuthor.Id,
                        BookId = book.Id
                    };
                    await repository.AddAsync(existingAuthor);
                }
            }
            await repository.AddAsync(book);
            await repository.SaveChangesAsync();
        }

        public async Task AddRoomAsync(EditRoomViewModel model)
        {
            var room = new StudyRoom
            {
                Name = model.Name,
                Description = model.Description,
                Capacity = model.Capacity,
                Floor = model.Floor,
                ImageUrl = model.ImageUrl,
                LibraryId = 1
            };
            await repository.AddAsync(room);
            await repository.SaveChangesAsync();
        }

        public async Task<ManageRentViewModel> ManageBookRentAsync()
        {
            var books = repository.All<Book>().Where(x => x.RenterId != null);
            int booksToReturn = 0;
            
            foreach(var book in books) 
            {
                DateTime rentalTime = DateTime.Now.AddMonths(-book.RentalTime);
                TimeSpan remainingTime = rentalTime - book.RentalDate.Value;
                int remainingDays = remainingTime.Days;
                if(remainingDays < 0 || remainingDays == 0)
                {
                    book.IsRented = false;
                    book.RentalDate = null;
                    book.RenterId = null;
                    booksToReturn++;
                }               
            }
            await repository.SaveChangesAsync();

            return new ManageRentViewModel
            {
                TotalBookRented = booksToReturn
            };
        }

		public async Task<ManageRentViewModel> ManageRoomRentAsync()
		{
			var rooms = repository.All<StudyRoom>().Where(x => x.RenterId != null);
            int roomsToReturn = 0;

            foreach (var room in rooms)
            {
                var rentalTime = DateTime.Now.AddDays(-room.RentalTime);
                var remainingTime = rentalTime - room.RentalDate.Value;
                var remainingDays = remainingTime.Days;
				if (remainingDays > 0 || remainingDays == 0)
				{
					room.IsRented = false;
					room.RentalDate = null;
					room.RenterId = null;
					roomsToReturn++;
				}
			}
			await repository.SaveChangesAsync();
			var model = new ManageRentViewModel
			{
				TotalRoomsRented = roomsToReturn
			};
			return model;
		}

		public async Task DeleteBookAsync(int id)
		{
			var book = await repository.All<Book>().Where(x => x.Id == id).FirstOrDefaultAsync();
            var bookAuthors = await repository.All<BookByBookAuthor>().Where(x => x.BookId == id).ToListAsync();
            foreach(var ba in bookAuthors)
            {
				await repository.DeleteAsync(ba);
			}
            if (book != null)
            {
				await repository.DeleteAsync(book);
			}
			await repository.SaveChangesAsync();
		}

		public async Task DeleteRoomAsync(int id)
		{
			var room = await repository.All<StudyRoom>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (room != null)
            {
				await repository.DeleteAsync(room);
			}
            await repository.SaveChangesAsync();
		}

		public async Task EditRoomAsync(int id, EditRoomViewModel model)
		{
			var room = repository.All<StudyRoom>().FirstOrDefault(x => x.Id == id);
			if (room == null)
            {
				throw new InvalidOperationException("Room not found");
			}
			room.Name = model.Name;
			room.Description = model.Description;
			room.Capacity = model.Capacity;
			room.Floor = model.Floor;
			room.ImageUrl = model.ImageUrl;
			room.IsRented = model.IsRented;
			await repository.SaveChangesAsync();
		}

        public async Task<bool> IsRoomRentedAsync(int id)
        {
           return await repository.AllReadOnly<StudyRoom>().Where(x => x.Id == id).Select(x => x.IsRented).FirstOrDefaultAsync();
        }

        public async Task<bool> IsRoomRentedByUserWithIdAsync(string userId, int id)
        {
            return await repository.AllReadOnly<StudyRoom>().Where(x => x.Id == id && x.RenterId == userId).AnyAsync();           
        }

        public async Task RentRoomAsync(string userId, int id)
        {
            var room = await repository.All<StudyRoom>().FirstOrDefaultAsync(x => x.Id == id);
            room.IsRented = true;
            room.RenterId = userId;
            room.RentalDate = DateTime.Now;
            await repository.SaveChangesAsync();
        }
    }
}
