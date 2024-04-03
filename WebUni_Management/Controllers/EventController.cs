using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Event;

namespace WebUni_Management.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            if (await eventService.EventExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await eventService.GetDetailsForEventById(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new EventFormViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            if (ModelState.IsValid == false) 
            { 
              return BadRequest();
            }
            await eventService.AddEventAsync(model);
            return RedirectToAction(nameof(AllEvents));
        }
        [HttpGet]
        public async Task<IActionResult> AllEvents([FromQuery] AllEventsShowcaseViewModel query)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var model = await eventService.FilterEventsAsunc(userId, query.SearchTerm, query.EventsPerPage, query.CurrentPage);
            query.Events = model.Events;
            query.TotalEvents = model.TotalEvents;
            return View(query);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await eventService.EventExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await eventService.GetEditEventFormAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventFormViewModel model)
        {
            if (await eventService.EventExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            await eventService.EditEventAsync(id, model);
            return RedirectToAction(nameof(AllEvents));
        }
        public async Task<IActionResult> Join(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await eventService.EventExistsByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if(await eventService.UserHasAlreadyJoinedEvent(id, userId) == true)
            {
                throw new InvalidOperationException();
            }
            await eventService.JoinEventAsync(id, userId);
            return RedirectToAction("JoinedEvents", "PersonalInfo", new {userId = userId});
        }
        public async Task<IActionResult> Delete(int id)
        {
			if (await eventService.EventExistsByIdAsync(id) == false)
			{
				return BadRequest();
			}
			await eventService.DeleteEventByIdAsync(id);
            return RedirectToAction(nameof(AllEvents));
        }
    }
}
