using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Event;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        public EventService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddEventAsync(EventFormViewModel model)
        {
            var ev = new Event()
            {
                Name = model.Name,
                Capacity = model.Capacity,
                GuestParticipant = model.GuestParticipant,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
            };
            await repository.AddAsync(ev);
            await repository.SaveChangesAsync();
        }

        public async Task<EventDetailsViewModel?> GetDetailsForEventById(int id)
		{
			return await repository.AllReadOnly<Event>()
                .Where(x => x.Id == id)
                .Select(x => new EventDetailsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Capacity = x.Capacity,
                    StartTime = x.StartTime.ToString("MMM dd, yyyy HH: mm"),
					EndTime = x.EndTime.ToString("MMM dd, yyyy HH: mm"),
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    GuestParticipant = x.GuestParticipant
				})
                .FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<EventIndexViewModel>> GetLastThreeEventsAsync(string userId)
        {
            var eventsOfUser = await repository.AllReadOnly<EventParticipant>().Where(x => x.ParticipantId == userId).Select(x => x.EventId).ToListAsync();

            return await repository.AllReadOnly<Event>()
                .Where(x => !eventsOfUser.Contains(x.Id))
                .Take(3)
                .Select(x => new EventIndexViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartTime = x.StartTime.ToString("MMM dd, yyyy HH:mm"),
                    EndTime = x.EndTime.ToString("HH:mm"),
                    GuestParticipant = x.GuestParticipant,
                    ImageUrl = x.ImageUrl,
                })
                .ToListAsync();
        }
    }
}
