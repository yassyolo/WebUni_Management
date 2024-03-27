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
        Task AddNewsAsync(NewsFormViewModel model);
        Task ApproveNewsArticleAsync(int id);
        Task DeleteNewsArticleAsync(int id);
		Task DiscardNewsArticleAsync(int id);
		Task EditNewsAsync(NewsFormViewModel model, int id);
        Task<bool> ExistByIdAsync(int id);
        Task<NewsShowcaseViewModel> FilterNewsAsync(string? yearSearchTerm, string? monthSearchTerm, string? dateSearchTerm, int currentPage, int newsPerPage);
        Task<NewsFormViewModel?> GetEditNewsFormAsync(int id);
        Task<IEnumerable<NewsArticleIndexViewModel>> GetLastThreeNewsArticlesAsync(string userId);
        Task<NewsDetailsViewModel?> GetNewsArticleDetailsById(int id, string userId);
        Task<ApproveNewsViewModel> GetNewsForApprovalAsync(int newsPerPage, int currentPage);
        Task WriteNewsAsync(NewsFormViewModel model, string userId);
	}
}
