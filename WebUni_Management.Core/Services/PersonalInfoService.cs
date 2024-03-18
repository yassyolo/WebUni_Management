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
    public class PersonalInfoService : IPersonalInfoService
	{
		private readonly IRepository repository;

		public PersonalInfoService(IRepository _repository)
		{
			repository = _repository;
		}

        public async Task<IEnumerable<BookInfoViewModel>> MyRentedBooksAsync(string userId)
        {
            return await repository.AllReadOnly<Book>()
                .Where(x => x.RenterId == userId)
                .Select(x => new BookInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = string.Join(", ", x.Author.Select(x => x.FirstName + " " + x.LastName)),
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.Name,
                }).ToListAsync();
        }

        public async Task RemoveBookRentAsync(int id, string userId)
        {
            var book =  repository.All<Book>().FirstOrDefault(x => x.Id == id && x.RenterId == userId);
            book.RenterId = null;
            book.IsRented = false;
            book.RentalDate = null;
            await repository.SaveChangesAsync();
        }

        public async Task<bool> RentedBookExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Book>().Where(x => x.Id == id).AnyAsync();
        }

        public async Task<bool> UserWithIdExistsAsync(string userId)
        {
            return await repository.AllReadOnly<Student>().Where(x => x.UserId == userId).AnyAsync();       
        }

        public async Task<bool> UserWithIdHasRentedBookAsync(int id, string userId)
        {
           return await repository.AllReadOnly<Book>().AnyAsync(x => x.Id == id && x.RenterId == userId);
        }
    }
}
