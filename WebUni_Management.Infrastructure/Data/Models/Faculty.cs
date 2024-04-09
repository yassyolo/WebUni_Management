using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Faculty entity")]
    public class Faculty
    {

        [Comment("Faculty identifier")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Comment("Faculty name")]
        [MaxLength(FacultyNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Faculty description")]
        [MaxLength(FacultyDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("Faculty majors")]
        public IEnumerable<Major> Majors { get; set; } = new List<Major>();
        [Comment("Faculty students")]
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
