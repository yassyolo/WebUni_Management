using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Core.Models.PersonalInfo
{
	public class SeeMySubjectDetailsViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int TotlaAttendanceCount { get; set; }

		public IEnumerable<SubjectProfessor> SubjectProfessors { get; set; } = new List<SubjectProfessor>();

		public string Faculty { get; set; } = string.Empty;
		public string Major { get; set; } = string.Empty;
		public string CourseTerm { get; set; } = string.Empty;
		public AssistantDetailsViewModel Assistant { get; set; } = null!;
		public ProfessorDetailsViewModel Professor { get; set; } = null!;
	}
}
