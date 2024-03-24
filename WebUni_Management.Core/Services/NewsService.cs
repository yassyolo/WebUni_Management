using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<NewsArticleIndexViewModel>> GetLastThreeNewsArticlesAsync(string userId)
        {
            return await repository.AllReadOnly<NewsArticle>()
                .OrderByDescending(x => x.PublishedOn)
                .Take(3)
                .Select(x => new NewsArticleIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                })
                .ToListAsync();
            /*foreach (var article in articles) 
            { 
                await repository.AllReadOnly<NewsArticleReadStatus>().
            }*/
        }

        public async Task<NewsDetailsViewModel?> GetNewsArticleDetailsById(int id, string userId)
        {
            var model = await repository.AllReadOnly<NewsArticle>().Where(x => x.Id == id)
                .Select(x => new NewsDetailsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    PublishedOn = x.PublishedOn.ToString(),
                    Content = x.Content
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
    }
}
