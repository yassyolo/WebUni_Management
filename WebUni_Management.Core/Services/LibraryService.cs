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
                    Category = x.Category.Name
                })
                .ToListAsync();
        }
    }
}
