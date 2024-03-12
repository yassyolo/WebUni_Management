using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Account;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repository;

        public AccountService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<Student?> FindUserByIdAsync(string user)
        {
            return await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x => x.UserId == user);
        }

        public async Task<IEnumerable<RequestsViewModel>> GetRequestsAsync()
        {
           return await repository.AllReadOnly<ApplicationUser>()
           .Where(x => x.IsApproved == false).Select(x => new RequestsViewModel
           {
                UserName = x.UserName,
                InitialPassword = x.InitialPassword,
                Email = x.Email
           }).ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByUserNameAsync(string username)
        {
            return await repository.AllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task UpdateUserAsync(string userId, ManageAccountViewModel model)
        {
            var user = await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x=> x.UserId == userId);   
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Age = model.Age;
            user.PhoneNumber = model.PhoneNumber;
            user.FacultyNumber = model.FacultyNumber;

            await repository.SaveChangesAsync();
        }
    }
}
