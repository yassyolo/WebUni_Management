using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.News;

namespace WebUni_Management.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService _newsService)
        {
            newsService = _newsService;
        }
        [HttpGet]   
        public async Task<IActionResult> Index([FromQuery]NewsShowcaseViewModel query)
        {
            var model = await newsService.FilterNewsAsync(query.YearSearchTerm, query.MonthSearchTerm, query.DateSearchTerm, query.CurrentPage, query.NewsPerPage);
            query.TotalNews = model.TotalNews;
            query.News = model.News;
            return View(query);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await newsService.GetNewsArticleDetailsById(id, userId);
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new NewsFormViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(NewsFormViewModel model)
        {
            if(!ModelState.IsValid) 
            {
                return View(model);
            }
            await newsService.AddNewsAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        { 
            if(await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await newsService.GetEditNewsFormAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(NewsFormViewModel model, int id)
        {
            if (await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }
            if(ModelState.IsValid == false) 
            { 
                return View(model);
            }

            await newsService.EditNewsAsync(id, model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult WriteNews()
        {
            var model = new NewsFormViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriteNews(NewsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await newsService.WriteNewsAsync(userId, model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ApproveNews([FromQuery] ApproveNewsViewModel query)
        {
            var model = await newsService.GetNewsForApprovalAsync(query.NewsPerPage, query.CurrentPage);
            query.TotalNews = model.TotalNews;
            query.News = model.News;

            return View(query);
        }
        public async Task<IActionResult> Approve(int id)
        {
            if( await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }
            await newsService.ApproveNewsArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }
            await newsService.DeleteNewsArticleAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Discard(int id)
        {
			if (await newsService.ExistByIdAsync(id) == false)
			{
				return BadRequest();
			}
			await newsService.DiscardNewsArticleAsync(id);
			return RedirectToAction(nameof(Index));
		}
    }
}
