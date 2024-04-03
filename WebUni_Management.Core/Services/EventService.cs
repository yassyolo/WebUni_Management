using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

		public async Task DeleteEventByIdAsync(int id)
		{
			var ev = await repository.All<Event>().Where(e => e.Id == id).FirstOrDefaultAsync();
            var eventParticipant = await repository.All<EventParticipant>().Where(x => x.EventId == id).ToListAsync();
			foreach (var evp in eventParticipant)
			{
				await repository.DeleteAsync(evp);
			}
			if (ev != null )
            {
                await repository.DeleteAsync(ev);
            }
           
            await repository.SaveChangesAsync();
		}

		public async Task EditEventAsync(int id, EventFormViewModel model)
        {
            var ev = await repository.All<Event>().FirstOrDefaultAsync(x => x.Id == id);
            ev.Description = model.Description;
            ev.Name = model.Name;
            ev.Capacity = model.Capacity;
            ev.GuestParticipant = model.GuestParticipant;
            ev.StartTime = model.StartTime;
            ev.EndTime = model.EndTime;

            await repository.SaveChangesAsync();
        }

        public async Task<bool> EventExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Event>().AnyAsync( x=> x.Id == id);
        }

        public async Task<AllEventsShowcaseViewModel> FilterEventsAsunc(string userId, string? searchTerm = null, int eventsPerPage = 2, int currentPage = 1)
        {
            var events = repository.AllReadOnly<Event>();

            if (searchTerm != null)
            {
                var normalizedSearchTerm = searchTerm.ToLower();
                events = events.Where(x => x.GuestParticipant.Contains(normalizedSearchTerm) || x.Name.Contains(normalizedSearchTerm));
            }
            var joinedEvents = await repository.AllReadOnly<EventParticipant>().Where(x => x.ParticipantId  == userId).Select(x=> x.EventId).ToListAsync();
            var eventsToShow = await events .Where(x => !joinedEvents.Contains(x.Id)).Skip((currentPage - 1) * eventsPerPage).Take(eventsPerPage)
               
                .Select(x => new EventIndexViewModel()
                {
                    Name = x.Name,
                    GuestParticipant = x.GuestParticipant,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    StartTime = x.StartTime.ToString("MMM dd, yyyy HH:mm"),
                    EndTime = x.EndTime.ToString("HH:mm"),
                }).ToListAsync();
            return new AllEventsShowcaseViewModel()
            {
                Events = eventsToShow,
                TotalEvents = await events.CountAsync()
            };
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

        public async Task<EventFormViewModel?> GetEditEventFormAsync(int id)
        {
            return await repository.AllReadOnly<Event>().Where(x => x.Id == id)
                .Select(x => new EventFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    GuestParticipant = x.GuestParticipant,
                    Capacity = x.Capacity,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EventIndexViewModel>> GetLastThreeEventsAsync(string userId)
        {
            //var eventsOfUser = await repository.AllReadOnly<EventParticipant>().Where(x => x.ParticipantId == userId).Select(x => x.EventId).ToListAsync();

            return await repository.AllReadOnly<Event>()
                //.Where(x => !eventsOfUser.Contains(x.Id))
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

        public async Task<IEnumerable<EventIndexViewModel>> JoinedEventsAsync(string userId)
        {
            return await repository.AllReadOnly<EventParticipant>().Where(x => x.ParticipantId == userId)
                 .Select(x => new EventIndexViewModel()
                 {
                     Id = x.EventId,
                     Name = x.Event.Name,
                     StartTime = x.Event.StartTime.ToString("MMM dd, yyyy"),
                     EndTime = x.Event.EndTime.ToString("MMM dd, yyyy"),
                     GuestParticipant = x.Event.GuestParticipant,
                     ImageUrl = x.Event.ImageUrl
                 })
                 .ToListAsync();
        }

        public async Task JoinEventAsync(int id, string userId)
        {
            var eventParticipant = new EventParticipant()
            {
                ParticipantId = userId,
                EventId = id
            };
           
            await repository.AddAsync(eventParticipant);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> UserHasAlreadyJoinedEvent(int id, string userId)
        {
            return await repository.AllReadOnly<EventParticipant>().AnyAsync(x => x.EventId == id && x.ParticipantId == userId);
        }
    }
}
