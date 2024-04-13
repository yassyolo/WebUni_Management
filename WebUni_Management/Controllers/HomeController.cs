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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INewsService newsService;
        private readonly IEventService eventService;

        public HomeController(INewsService _newsService, IEventService _eventService)
        {
            newsService = _newsService;
            eventService = _eventService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new IndexPageViewModel();
            model.News = await newsService.GetLastThreeNewsArticlesAsync();
            model.Events = await eventService.GetLastThreeEventsAsync();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Back(string previousPage)
        {
            if (string.IsNullOrEmpty(previousPage))
            {
                return RedirectToAction(nameof(Index));
            }

            var controllerAndAction = previousPage.Split("/");
            if (controllerAndAction.Length == 2)
            {
                return RedirectToAction(controllerAndAction[1], controllerAndAction[0]);
            }
            else if(controllerAndAction.Length == 3)
            {
                return RedirectToAction(controllerAndAction[0], controllerAndAction[1], new { id = controllerAndAction[2] });
            }
            else if(controllerAndAction.Length == 5)
            {
				return RedirectToAction(controllerAndAction[0], controllerAndAction[1], new { id = controllerAndAction[2], previousPage = $"{controllerAndAction[3]}/{controllerAndAction[4]}" });
			}

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            else if (statusCode == 401)
            {
                return View("Error401");
            }
            else if (statusCode == 500)
            {
                return View("Error500");
            }
            return View();
        }
    }
}
