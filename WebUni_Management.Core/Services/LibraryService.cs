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

        public async Task<AllBooksQueryModel> AllBooksAsync(string? category = null, string? searchTerm = null, int currentPage = 1, int booksPerPage = 1)
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
            return new AllBooksQueryModel
            {
                Books = booksToShow,
                TotalBooksCount = await books.CountAsync(),
            };
        }

        public async Task<IEnumerable<string>> AllCategorisNamesAsync()
        {
            return await repository.AllReadOnly<BookCategory>().Select(x => x.Name).ToListAsync();        
        }

        public async Task<BookDetailsViewModel?> BookDetailsAsync()
        {
            return await repository.AllReadOnly<Book>()
                .Select(x => new BookDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = string.Join(", ", x.Author.Select(x => x.FirstName + " " + x.LastName)),
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

		public async Task<bool> IsBookRentedAsync(int id)
		{
			return await repository.AllReadOnly<Book>().Where(x => x.Id == id).Select(x => x.IsRented).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<BookInfoViewModel>> LastThreeBooksAsync()
        {
           return await repository.AllReadOnly<Book>()
                .OrderByDescending(x => x.Id)
                .Take(3)
                .Select(x => new BookInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = string.Join(", ", x.Author.Select(x => x.FirstName + " " + x.LastName)),
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl
                })
                .ToListAsync();
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
	}
}
