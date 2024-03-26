using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Event
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string GuestParticipant { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
