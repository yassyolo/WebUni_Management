using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Subject;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Professor of subject entity")]
    public class SubjectProfessor
    {
        [Key]
        [Comment("Subject Professor identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(SubjectProfessorNameMaxLength)]
        [Comment("Professor first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectProfessorNameMaxLength)]
        [Comment("Professor last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectProfessorEmailMaxLength)]
        [DataType(DataType.EmailAddress)]
        [Comment("Professor email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectProfessorPhoneNumberMaxLength)]
        [DataType(DataType.PhoneNumber)]
        [Comment("Professor email")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectProfessorTitleMaxLength)]
        [Comment("Professor title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(SubjectProfessorDescriptionMaxLength)]
        [Comment("Professor description")]
        public string Description { get; set; } = string.Empty;

        [Comment("Subjects of the professor")]
        public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
