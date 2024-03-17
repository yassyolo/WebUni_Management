using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;
namespace WebUni_Management.Core.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(FacultyNumberLength, ErrorMessage = UserNameLengthErrorMessage)]
        [Display(Name = "User Name")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        //public bool RememberMe { get; set; }
    }
}
