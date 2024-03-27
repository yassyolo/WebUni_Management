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
        Task EditEventAsync(int id, EventFormViewModel model);
        Task<bool> EventExistsByIdAsync(int id);
        Task<AllEventsShowcaseViewModel> FilterEventsAsunc(string userId, string searchTerm, int eventsPerPage, int currentPage);
		Task<EventDetailsViewModel?> GetDetailsForEventById(int id);
        Task<EventFormViewModel?> GetEditEventFormAsync(int id);
        Task<IEnumerable<EventIndexViewModel>> GetLastThreeEventsAsync(string userId);
        Task<IEnumerable<EventIndexViewModel>> JoinedEventsAsync(string userId);
        Task JoinEventAsync(int id, string userId);
        Task<bool> UserHasAlreadyJoinedEvent(int id, string userId);
    }
}
