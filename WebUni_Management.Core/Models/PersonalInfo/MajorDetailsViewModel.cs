using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class MajorDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty; 
        public IEnumerable<SubjectIndexViewModel> Subjects { get; set; } = new List<SubjectIndexViewModel>();
    }
}
