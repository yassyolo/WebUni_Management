using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Subject for student entity")]
    public class SubjectForStudent
    {
        [Comment("Subject identifier")]
        public int SubjectId { get; set; }

        [Comment("Subject")]
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;

        [Comment("Student identifier")]
        public string StudentId { get; set; } = string.Empty;

        [Comment("Student")]
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser Student { get; set; } = null!;

        [Comment("AttendanceRecord")]
        public int? AttendanceRecord { get; set; }
    }
}
