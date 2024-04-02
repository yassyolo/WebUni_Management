using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Student entity")]
    public class Student
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        [Comment("Student first name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(NameMaxLength)]
        [Comment("Student last name")]
        public string LastName { get; set; } = string.Empty;

        [Range(AgeMinValue, AgeMaxValue)]
        [Comment("Student age")]
        public int Age { get; set; }

        [Phone]
        [Comment("Student phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(FacultyNumberLength)]
        [Comment("Student faculty number")]
        public string FacultyNumber { get; set; } = string.Empty;

        [Comment("User")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("User id")]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        [Comment("Rented books")]
        public IEnumerable<Book>? RentedBooks { get; set; } = new List<Book>();

        [Comment("Rented rooms")]
        public IEnumerable<StudyRoom>? RentedStudyRooms { get; set; } = new List<StudyRoom>();

        [Comment("News articles by the student")]
        public IEnumerable<NewsArticle>? NewsArticles { get; set; } = new List<NewsArticle>();

        [Comment("Subjects of the student")]
        public IEnumerable<Subject>? Subjects { get; set; } = new List<Subject>();
    }
}
