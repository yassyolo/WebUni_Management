using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class ManageAttendanceViewModel
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;
        public string FacultyNumber { get; set; } = string.Empty;
        public int SubjectId { get; set; } 
        public string SubjectName { get; set; } = string.Empty;
        public int SubjectTotalAttendance { get; set; }
        public int? StudentAttendanceRecord { get; set; }
        public int? RemainingAttendance { get; set; }

    }
}
