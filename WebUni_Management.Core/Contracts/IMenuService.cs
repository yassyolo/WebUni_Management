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
		Task ChangeMenuDateAsync(int id);
		Task<bool> DishExistsById(int id);
		Task EditDishAsync(int id, DishFormViewModel model);
		Task<DishFormViewModel?> GetDishForEditAsync(int id);
		Task<MenuIndexViewModeltest> GetMenuAsync();
		Task<bool> MenuExistsById(int id);
	}
}
