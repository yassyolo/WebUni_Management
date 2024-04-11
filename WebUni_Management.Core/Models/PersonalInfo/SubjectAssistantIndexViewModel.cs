using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Subject;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

namespace WebUni_Management.Core.Models.PersonalInfo
{
	public class SubjectAssistantIndexViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorNameMaxLength, MinimumLength = SubjectProfessorNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorNameMaxLength, MinimumLength = SubjectProfessorNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorEmailMaxLength, MinimumLength = SubjectProfessorNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorPhoneNumberMaxLength, MinimumLength = SubjectProfessorPhoneNumberMinLength, ErrorMessage = MaxLengthErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorTitleMaxLength, MinimumLength = SubjectProfessorTitleMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SubjectProfessorDescriptionMaxLength, MinimumLength = SubjectProfessorDescriptionMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
