using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
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
    }
}
