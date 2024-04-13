using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Student;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

namespace WebUni_Management.Core.Models.PersonalInfo
{
	public class SubjectGradeViewModel
	{
		public int StudentId { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(GradeMinValue, GradeMaxValue, ErrorMessage = InvalidGrade)]
		public double Grade { get; set; }
	}
}
