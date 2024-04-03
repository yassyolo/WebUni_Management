using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Subject;
using static WebUni_Management.Core.Models.Constants.MessageConstants;

namespace WebUni_Management.Core.Models.PersonalInfo
{
	public class EditSubjectFormViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[MaxLength(SubjectNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(SubjectDescriptionMaxLength, MinimumLength =SubjectDescriptionMinLength, ErrorMessage =MaxLengthErrorMessage)]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(SubjectMinAttendanceTimes, SubjectMaxAttendanceTimes, ErrorMessage =MaxLengthErrorMessage)]
		public int TotalAttendanceCount { get; set; }
		public IEnumerable<SubjectProfessorIndexViewModel> SubjectProfessors { get; set; } = new List<SubjectProfessorIndexViewModel>();
	}
}
