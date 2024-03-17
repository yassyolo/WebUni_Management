using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Library;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Book author entity")]
    public class BookAuthor
    {
        [Comment("Book author identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Book author first name")]
        [MaxLength(BookAuthorMaxNameLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Comment("Book author last name")]
        [MaxLength(BookAuthorMaxNameLength)]
        public string LastName { get; set; } = string.Empty;

        [Comment("Books by the author")]
        public IEnumerable<Book> Books { get; set; } = null!;
    }
}
