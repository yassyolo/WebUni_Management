using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Library;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Book entity")]
    public class Book
    {
        [Required]
        [Comment("Book Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength)]
        [Comment("Book title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment("Book image URL")]
        [MaxLength(BookImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;

        [Comment("Book author")]
        public IEnumerable<BookAuthor> Author { get; set; } = new List<BookAuthor>();

        [Required]
        [Comment("Book category")]
        public BookCategory Category { get; set; } = null!;

        [Comment("Book category identifier")]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(BookPublishYearMaxLength)]
        [Comment("Book publish year")]
        public string PublishYear { get; set; } = string.Empty;

        [Required]
        [MaxLength(BookDescriptionMaxLength)]
        [Comment("Book description")]
        public string Description { get; set; } = string.Empty;

        [Comment("Is book rented")]
        public bool IsRented { get; set; }

        [Comment("Book renter")]
        [ForeignKey(nameof(RenterId))]
        public ApplicationUser? Renter { get; set; }

        [Comment("Book renter identifier")]
        public string? RenterId { get; set; }

		[Comment("Rent date of the book")]
        public DateTime? RentalDate { get; set; }

        [Comment("Library identifier")]
        public int? LibraryId { get; set; }

        [Comment("Library")]
        [ForeignKey(nameof(LibraryId))]
        public Library Library { get; set; } = null!;

        [Comment("Rental time") ]
        [DefaultValue(RentalTimeDefaultValue)]
        public int RentalTime { get; set; }
    }
}
