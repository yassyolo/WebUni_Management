using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Event
{
	public class AllEventsShowcaseViewModel
	{
		public int EventsPerPage { get; set; } = 2;

		public int CurrentPage { get; set; } = 1;
		public string SearchTerm { get; set; } = string.Empty;
        public int TotalEvents { get; set; }
		public IEnumerable<EventIndexViewModel> Events { get; set; } = new List<EventIndexViewModel>();
    }
}
