using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class MyRentedBooksViewModel
    {
        public int BooksPerPage { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public int TotalBooks { get; set; }
        public IEnumerable<BookInfoViewModel> Books { get; set; } = new List<BookInfoViewModel>();
    }
}
