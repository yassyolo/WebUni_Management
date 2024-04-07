using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class AllMajorsViewModel
    {
        public int MajorsPerPage { get; set; } = 4;
        public int CurrentPage { get; set; } = 1;
        public int TotalMajors { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public IEnumerable<MajorIndexViewModel> Majors { get; set; } = new List<MajorIndexViewModel>();
    }
}
