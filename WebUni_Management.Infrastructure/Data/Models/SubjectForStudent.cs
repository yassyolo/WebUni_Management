﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;

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
        [Range(AttendanceMinValue, AttendanceMaxValue)]
        public int? AttendanceRecord { get; set; }

        [Comment("Grade")]
        [Range(GradeMinValue, GradeMaxValue)]
        public double? Grade { get; set; }
    }
}
