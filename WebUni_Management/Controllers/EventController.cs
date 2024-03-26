using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction(nameof(Index));
        }
    }
}
