using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class PersonalInfoViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FacultyNumber { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
		public string CourseTerm { get; set; } = string.Empty;
		public IEnumerable<SubjectIndexViewModel> Subjects { get; set; } = new List<SubjectIndexViewModel>();
    }
}
