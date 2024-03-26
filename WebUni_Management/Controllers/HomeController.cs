using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models;
using WebUni_Management.Core.Models.Home;

namespace WebUni_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly INewsService newsService;
        private readonly IEventService eventService;

        public HomeController(ILogger<HomeController> _logger, INewsService _newsService, IEventService _eventService)
        {
            logger = _logger;
            newsService = _newsService;
            eventService = _eventService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new IndexPageViewModel();
            model.News = await newsService.GetLastThreeNewsArticlesAsync(userId);
            model.Events = await eventService.GetLastThreeEventsAsync(userId);
            return View(model);
        }
    }
}
