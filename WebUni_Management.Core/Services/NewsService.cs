using Microsoft.EntityFrameworkCore;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.News;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
	public class NewsService : INewsService
    {
        private readonly IRepository repository;

        public NewsService(IRepository _repository)
        {
           repository = _repository;
        }

        public async Task AddNewsAsync(NewsFormViewModel model)
        {
            var newsArticle = new NewsArticle()
            {
                Title = model.Title,
                PublishedOn = DateTime.Parse(model.PublishedOn),
                Content = model.Content,
                ImageUrl = model.ImageUrl,
                IsApproved = true,
            };
            await repository.AddAsync(newsArticle);
            await repository.SaveChangesAsync();
        }

        public async Task ApproveNewsArticleAsync(int id)
        {
            var newsArticle = await repository.All<NewsArticle>().Where(x => x.Id == id).FirstOrDefaultAsync();
            newsArticle.IsApproved = true;
            await repository.SaveChangesAsync();
        }

        public async Task DeleteNewsArticleAsync(int id, string userId)
        {
            var newsArticle = await repository.All<NewsArticle>().FirstOrDefaultAsync(x => x.Id == id);
            var newsArticleStatus = await repository.All<NewsArticleReadStatus>().Where(x => x.NewsArticleId == id && x.ReaderId == userId).ToListAsync();
            foreach (var item in newsArticleStatus)
            {
                await repository.DeleteAsync<NewsArticleReadStatus>(item);
            }
            await repository.DeleteAsync<NewsArticle>(newsArticle);
        }

		public async Task DiscardNewsArticleAsync(int id)
		{
			var newsArticle = await repository.All<NewsArticle>().FirstOrDefaultAsync(x => x.Id == id);
            await repository.DeleteAsync<NewsArticle>(newsArticle);
		}

		public async Task EditNewsAsync(int id, NewsFormViewModel model)
        {
            var newsArticle = await repository.All<NewsArticle>().FirstOrDefaultAsync(x => x.Id == id);

            newsArticle.Title = model.Title;
            newsArticle.Content = model.Content;
            newsArticle.ImageUrl = model.ImageUrl;
            newsArticle.PublishedOn = DateTime.Parse(model.PublishedOn);

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await repository.AllReadOnly<NewsArticle>().AnyAsync(x => x.Id == id);
        }

        public async Task<NewsShowcaseViewModel> FilterNewsAsync(string? yearSearchTerm = null, string? monthSearchTerm = null, string? dateSearchTerm = null, int currentPage = 1, int newsPerPage = 2)
        {
            var news = repository.AllReadOnly<NewsArticle>();

            if(yearSearchTerm != null && yearSearchTerm != "")
            {
                news = news.Where(x => x.PublishedOn.Year.ToString() == yearSearchTerm);
            }
            if (monthSearchTerm != null && monthSearchTerm != "")
            {
                news = news.Where(x => x.PublishedOn.Month.ToString() == monthSearchTerm);
            }
            if (dateSearchTerm != null && dateSearchTerm != "")
            {
                news = news.Where(x => x.PublishedOn.Date.ToString() == dateSearchTerm);
            }
            var newsToShow = await news.Skip((currentPage - 1) * newsPerPage)
                .Take(newsPerPage)
                .Where(x => x.IsApproved != false)
                .Select(x => new NewsDetailsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    PublishedOn = x.PublishedOn.ToString("MMM dd, yyyy"),
                    Content = x.Content
                })
                .ToListAsync();

            return new NewsShowcaseViewModel
            {
                TotalNews = await news.CountAsync(),
                News = newsToShow
            };
        }

        public async Task<NewsFormViewModel?> GetEditNewsFormAsync(int id)
        {
            return await repository.AllReadOnly<NewsArticle>().Where(x => x.Id == id)
                .Select(x => new NewsFormViewModel()
                {
                    Title = x.Title,
                    Id = x.Id,
                    ImageUrl= x.ImageUrl,
                    Content = x.Content,
                    PublishedOn = x.PublishedOn.ToString()
                })
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<NewsArticleIndexViewModel>> GetLastThreeNewsArticlesAsync()
        {
            return await repository.AllReadOnly<NewsArticle>()
                .Where(x => x.IsApproved != false)
                .OrderByDescending(x => x.PublishedOn)
                .Take(3)
                .Select(x => new NewsArticleIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                })
                .ToListAsync();
        }

        public async Task<NewsDetailsViewModel?> GetNewsArticleDetailsById(int id, string userId)
        {
            var model = await repository.AllReadOnly<NewsArticle>().Where(x => x.Id == id)
                .Select(x => new NewsDetailsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    PublishedOn = x.PublishedOn.ToString("MMM dd, yyyy"),
                    Content = x.Content,
                    Author = x.Author.FirstName + " " + x.Author.LastName
                })
                .FirstOrDefaultAsync();
            var newsArticleStatusById = await repository.All<NewsArticleReadStatus>().Where(x => x.NewsArticleId == id && x.Read == false).FirstOrDefaultAsync();
            if(newsArticleStatusById != null)
            {
                newsArticleStatusById.ReaderId = userId;
                newsArticleStatusById.Read = true;
            }
            return model;
        }

        public async Task<ApproveNewsViewModel> GetNewsForApprovalAsync(int newsPerPage = 2, int currentPage = 1)
        {
            var news = repository.AllReadOnly<NewsArticle>().Where(x => x.IsApproved == false);
            var newsToShow = await news.Skip((currentPage - 1) * newsPerPage).Take(newsPerPage).
                Select(x => new NewsDetailsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    Content = x.Content,
                    PublishedOn = x.PublishedOn.ToString("MMM dd, yyyy")
                }).ToListAsync();
            return new ApproveNewsViewModel()
            {
                News = newsToShow,
                TotalNews = await news.CountAsync()
            };
        }

        public async Task WriteNewsAsync(string userId, NewsFormViewModel model)
		{
           
			var author = await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x => x.UserId == userId);
            var newsArticle = new NewsArticle()
			{
				Title = model.Title,
				PublishedOn = DateTime.Parse(model.PublishedOn),
				Content = model.Content,
				ImageUrl = model.ImageUrl,
                AuthorId = author.Id,
                IsApproved = false
			};
			await repository.AddAsync(newsArticle);
			await repository.SaveChangesAsync();
		}
	}
}
