using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Event;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class MyJoinedEventsViewModel
    {
        public int EventsPerPage { get; set; } = 2;
        public int CurrentPage { get; set; } = 1;
        public int TotalEvents { get; set; }
        public IEnumerable<EventIndexViewModel> Events { get; set; } = new List<EventIndexViewModel>();
    }
}
