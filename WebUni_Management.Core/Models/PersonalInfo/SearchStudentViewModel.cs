using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class SearchStudentViewModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public StudentIndexViewModel Student { get; set; } = null!;
    }
}
