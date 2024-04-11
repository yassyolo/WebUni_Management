using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Course term entity")]
    public class CourseTerm
    {
        [Key]
        [Comment("Course term identifier")]
        public int Id { get; set; }

        [MaxLength(CourseTermNameMaxLength)]
        [Comment("Course term name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Major course term")]
        public Major Major { get; set; } = null!;

        [Comment("Course major identifier")]
        [ForeignKey(nameof(Major))]
        public int MajorId { get; set; }

        [Comment("Students in the course term")]
        public IEnumerable<Student> Students { get; set; }= new List<Student>();
    }
}
