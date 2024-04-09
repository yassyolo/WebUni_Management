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
        Task<AllBooksQueryModel> AllBooksAsync(string? category = null, string? searchTerm = null, int currentPage = 1, int booksPerPage = 1);
        Task<IEnumerable<string>> AllCategorisNamesAsync();
        Task<IEnumerable<BookCategoryViewModel>> AllCategoriesForEditAsync();
        Task<BookDetailsViewModel?> BookDetailsAsync(int id);
        Task<bool> BookExistsByIdAsync(int id);
        Task<string> GetAuthor(int id);
        Task<EditBookViewModel> GetEditFormBookModelAsync(int id);
        Task<bool> IsBookRentedAsync(int id);
        Task<IEnumerable<BookInfoViewModel>> LastThreeBooksAsync();
        Task<IEnumerable<StudyRoomInfo>> LastThreeStudyRoomsAsync();
        Task RentBookAsync(int id, string userId);
        Task<bool> CategoryExistsById(int id);
        Task EditBookAsync(int id, EditBookViewModel model);
        Task<AllRoomsQueryModel> AllRoomsAsync(int? capacity = null, string? searchTerm = null, int currentPage = 1, int roomsPerPage = 1);
        Task<bool> RoomExistsByIdAsync(int id);
        Task<RoomShowcaseViewModel?> GetRoomDetailsByIdAsync(int id);
        Task<EditRoomViewModel?> GetEditRoomAsync(int id);
        Task AddBookAsync(EditBookViewModel model);
        Task AddRoomAsync(EditRoomViewModel model);
        Task<ManageRentViewModel> ManageBookRentAsync();
		Task<ManageRentViewModel> ManageRoomRentAsync();
		Task DeleteBookAsync(int id);
		Task DeleteRoomAsync(int id);
		Task EditRoomAsync(int id, EditRoomViewModel model);
        Task<bool> IsRoomRentedAsync(int id);
        Task<bool> IsRoomRentedByUserWithIdAsync(string userId, int id);
        Task RentRoomAsync(string userId, int id);
    }
}
