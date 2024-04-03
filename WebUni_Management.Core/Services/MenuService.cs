using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Menu;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepository repository;

        public MenuService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<MenuIndexViewModeltest> GetMenuAsync()
        {
            var menu = await repository.AllReadOnly<Menu>().Where(x => x.Id == 1).FirstOrDefaultAsync();
            var dishes = await repository.AllReadOnly<Dish>()
                .Select(x => new MenuItemsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price.ToString(),
                    Category = x.Category,
                }).ToListAsync();
            return new MenuIndexViewModeltest
            {
                Id = menu.Id,
                Dishes = dishes,
                Date = menu.Date.ToString("MMM dd, yyyy")
            };
        }

		/*/*public async Task<MenuFormViewModel?> GetMenuFormForUpdateAsync()
        {
           /* return await repository.AllReadOnly<Menu>().Where(x => x.Date == DateTime.Today)
                .Select(x => new MenuFormViewModel()
                {
                    MenuId = x.Id,
                    Date = x.Date
                })
                .FirstOrDefaultAsync();
            menu.Dishes = await repository.AllReadOnly<Dish>().Where(x => x.MenuId == menu.Id)
                .Select(x => new MenuItemsFormViewModel()
                {
                    Id = x.Id,
                    Category = x.Category,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();*/

	
    }
}
