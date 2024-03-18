using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;

namespace WebUni_Management.Core.Contracts
{
    public interface IPersonalInfoService
    {
        Task<IEnumerable<BookInfoViewModel>> MyRentedBooksAsync(string userId);
        Task RemoveBookRentAsync(int id, string userId);
        Task<bool> RentedBookExistsByIdAsync(int id);
        Task<bool> UserWithIdExistsAsync(string userId);
        Task<bool> UserWithIdHasRentedBookAsync(int id, string userId);
    }
}
