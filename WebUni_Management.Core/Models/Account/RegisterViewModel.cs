using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants;

namespace WebUni_Management.Core.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailErrorMessage)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(FacultyNumberLength, ErrorMessage = UserNameLengthErrorMessage)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string InitialPassword { get; set; } = string.Empty;
    }
}
