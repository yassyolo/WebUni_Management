using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;


namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Major entity")]
    public class Major
    {
        [Comment("Major identifier")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Comment("Major name")]
        [MaxLength(MajorNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Major description")]
        [MaxLength(MajorDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("Major faculty")]
        public Faculty Faculty { get; set; } = null!;

        [Comment("Faculty identifier")]
        [ForeignKey(nameof(Faculty))]
        public int FacultyId { get; set; }

        [Comment("Course terms in the major")]
        public IEnumerable<CourseTerm> CourseTerms { get; set; } = new List<CourseTerm>();

        [Comment("Students in the major")]
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
