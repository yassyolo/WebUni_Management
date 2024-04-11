using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Subject;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Subject entity")]
    public class Subject
    {
        [Key]
        [Comment("Subject identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(SubjectNameMaxLength)]
        [Comment("Subject name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectDescriptionMaxLength)]
        [Comment("Subject description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(SubjectMinAttendanceTimes, SubjectMaxAttendanceTimes)]
        [Comment("Subject attendance times")]
        public int TotlaAttendanceCount { get; set; }

        [Required]
        [Comment("Subject professors")]
        public IEnumerable<SubjectProfessor> SubjectProfessors { get; set; } = new List<SubjectProfessor>();

        [Comment("Subject faculty identifier")]
        public int FacultyId { get; set; }

        [ForeignKey(nameof(FacultyId))]
        [Comment("Subject faculty")]
        public Faculty Faculty { get; set; } = null!;

        [Comment("Subject major identifier")]
        public int MajorId { get; set; }

        [ForeignKey(nameof(MajorId))]
        [Comment("Subject major")]
        public Major Major { get; set; } = null!;

        [Comment("Subject course term")]
        [ForeignKey(nameof(CourseTermId))]
        public CourseTerm CourseTerm { get; set; } = null!;

        [Comment("Subject course term identifier")]
        public int CourseTermId { get; set; }

        [Comment("Subject students")]
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
