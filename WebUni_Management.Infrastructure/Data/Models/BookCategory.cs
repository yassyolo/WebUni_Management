using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Library;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Book category entity")]
    public class BookCategory
    {
        [Required]
        [Comment("Book category identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Book category name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Books in this category")]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
