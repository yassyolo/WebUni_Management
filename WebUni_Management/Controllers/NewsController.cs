using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.News;
using WebUni_Management.Extenstions;

namespace WebUni_Management.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService _newsService)
        {
            newsService = _newsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery]NewsShowcaseViewModel query)
        {
            var model = await newsService.FilterNewsAsync(query.YearSearchTerm, query.MonthSearchTerm, query.DateSearchTerm, query.CurrentPage, query.NewsPerPage);
            query.TotalNews = model.TotalNews;
            query.News = model.News;

            return View(query);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string previousPage)
        {
            if (await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }

            var userId = User.GetId();
            var model = await newsService.GetNewsArticleDetailsById(id, userId);
            ViewBag.PreviousPage = previousPage;

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var model = new NewsFormViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewsFormViewModel model)
        {
            if(ModelState.IsValid == false) 
            {
                return View(model);
            }
            await newsService.AddNewsAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, string previousPage) 
        { 
            if(await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await newsService.GetEditNewsFormAsync(id);
            ViewBag.PreviousPage = previousPage;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
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
        [Authorize(Roles = "Student")]
        public IActionResult WriteNews()
        {
            var model = new NewsFormViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteNews(NewsFormViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var userId = User.GetId();
            await newsService.WriteNewsAsync(userId, model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveNews([FromQuery] ApproveNewsViewModel query)
        {
            var model = await newsService.GetNewsForApprovalAsync(query.NewsPerPage, query.CurrentPage);
            query.TotalNews = model.TotalNews;
            query.News = model.News;

            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            if( await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }

            await newsService.ApproveNewsArticleAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await newsService.ExistByIdAsync(id) == false)
            {
                return BadRequest();
            }

            await newsService.DeleteNewsArticleAsync(id, User.GetId());

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
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
