using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Menu;

namespace WebUni_Management.Core.Contracts
{
    public interface IMenuService
    {
        Task<MenuIndexViewModeltest> GetMenuAsync();
        //Task<MenuFormViewModel?> GetMenuFormForUpdateAsync();
    }
}
