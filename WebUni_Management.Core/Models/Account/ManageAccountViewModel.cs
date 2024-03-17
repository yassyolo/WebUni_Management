using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

namespace WebUni_Management.Core.Models.Account
{
    public class ManageAccountViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)] 
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = UserNameLengthErrorMessage)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = UserNameLengthErrorMessage)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(AgeMinValue, AgeMaxValue, ErrorMessage = InvalidAgeErrorMessage)]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Phone(ErrorMessage = InvalidPhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(FacultyNumberLength, ErrorMessage = InvalidFacultyNumber)]
        [Display(Name = "Faculty number")]
        public string FacultyNumber { get; set; } = string.Empty;

    }
}
