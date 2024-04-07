using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class AllFacultiesViewModel
    {
        public int FacultiesPerPage { get; set; } = 4;
        public int CurrentPage { get; set; } = 1;
        public int TotalFaculties { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public IEnumerable<FacultyIndexViewModel> Faculties { get; set; } = new List<FacultyIndexViewModel>();
    }
}
