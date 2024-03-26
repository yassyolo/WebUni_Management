using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Event;

namespace WebUni_Management.Core.Contracts
{
    public interface IEventService
    {
        Task AddEventAsync(EventFormViewModel model);
        Task<EventDetailsViewModel?> GetDetailsForEventById(int id);
		Task<IEnumerable<EventIndexViewModel>> GetLastThreeEventsAsync(string userId);
    }
}
