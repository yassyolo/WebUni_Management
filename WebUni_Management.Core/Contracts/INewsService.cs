using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.News;

namespace WebUni_Management.Core.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<NewsArticleIndexViewModel>> GetLastThreeNewsArticlesAsync(string userId);
        Task<NewsDetailsViewModel?> GetNewsArticleDetailsById(int id, string userId);
    }
}
