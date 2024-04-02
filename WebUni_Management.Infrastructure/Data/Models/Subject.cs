using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
