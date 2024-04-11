using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Book by book author entity")]
    public class BookByBookAuthor
    {
        [Comment("Book identifier")]
        public int BookId { get; set; }

        [Comment("Book")]
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;

        [Comment("Book author identifier")]
        public int AuthorId { get; set; }

        [Comment("Book author")]
        [ForeignKey(nameof(AuthorId))]
        public BookAuthor Author { get; set; } = null!;
    }
}
