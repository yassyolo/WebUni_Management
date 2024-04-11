using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Library entity")]
    public class Library
    {
        [Comment("Library identifier")]
        [Required]
        public int Id { get; set; }

        [Comment("Books in the library")]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        [Comment("Study rooms in the library")]
        public IEnumerable<StudyRoom> StudyRooms { get; set; } = new List<StudyRoom>();
    }
}
