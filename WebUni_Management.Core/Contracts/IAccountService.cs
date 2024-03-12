using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Account;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Core.Contracts
{
    public interface IAccountService
    {
        Task<Student?> FindUserByIdAsync(string user);

        //Task ApproveAndUpdateUserAsync();
        Task<IEnumerable<RequestsViewModel>> GetRequestsAsync();
        Task<ApplicationUser?> GetUserByUserNameAsync(string username);
        Task UpdateUserAsync(string userId, ManageAccountViewModel model);
    }
}
