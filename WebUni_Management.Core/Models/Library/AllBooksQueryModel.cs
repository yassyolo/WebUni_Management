using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Library
{
    public class AllBooksQueryModel
    {
        public int BooksPerPage { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public int Id { get; set; }
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public int TotalBooksCount { get; set; }
        public IEnumerable<string>? Categories { get; set; } = new List<string>();
        public IEnumerable<BookShowcaseViewModel> Books { get; set; } = new List<BookShowcaseViewModel>();
    }
}
