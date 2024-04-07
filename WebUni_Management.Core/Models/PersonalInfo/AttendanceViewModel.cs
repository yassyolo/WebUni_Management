using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class AttendanceViewModel
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Date { get; set; } = string.Empty;

        //public IEnumerable<AttendanceItemsViewModel> Attendances = new List<AttendanceItemsViewModel>();
    }
}
