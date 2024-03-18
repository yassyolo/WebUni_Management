using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;

namespace WebUni_Management.Core.Contracts
{
    public interface ILibraryService
    {
        Task<AllBooksQueryModel> AllBooksAsync(string? category = null, string? searchTerm = null, int currentPage = 1, int housesPerPage = 1);
        Task<IEnumerable<string>> AllCategorisNamesAsync();
        Task<BookDetailsViewModel?> BookDetailsAsync();
        Task<bool> BookExistsByIdAsync(int id);
		Task<bool> IsBookRentedAsync(int id);
		Task<IEnumerable<BookInfoViewModel>> LastThreeBooksAsync();
        Task<IEnumerable<StudyRoomInfo>> LastThreeStudyRoomsAsync();
        Task RentBookAsync(int id, string userId);
	}
}
