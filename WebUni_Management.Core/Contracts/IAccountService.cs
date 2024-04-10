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
        Task AddNewStudentAsync(string username, ManageAccountViewModel model);
        Task<ManageAccountViewModel> FillManageAccountAsync(string userId);
		Task<ApplicationUser?> FindUserByIdAsync(string user);
        Task<AllRequestsViewModel> GetAllRequestsAsync(int currentPage, int requestsPerPage);
        Task<bool> GetStudentAsync(string userId);
        Task<ApplicationUser?> GetUserByUserNameAsync(string username);
        Task AddStudentAsync(string userId, ManageAccountViewModel model);
		Task<bool> StudentExistsByIdAsync(int id);
		Task<ManageAccountViewModel?> GetEditAccountFormAsync(int id);
        Task EditAccountAsync(int id, ManageAccountViewModel model);
        public Task<string> GetQrCodeForStudentAsync(string userId);
    }
}
