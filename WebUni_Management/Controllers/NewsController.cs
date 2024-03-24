using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;

namespace WebUni_Management.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService _newsService)
        {
            newsService = _newsService;
        }

        public IActionResult Index()
        {
            return View();
        }
       public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await newsService.GetNewsArticleDetailsById(id, userId);
            return View(model);
        }
    }
}
