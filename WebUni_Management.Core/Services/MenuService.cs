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

		public Task ChangeMenuDateAsync(int id)
		{
			var menu = repository.All<Menu>().FirstOrDefault(x => x.Id == id);
			menu.Date = DateTime.Now;
			return repository.SaveChangesAsync();
		}

		public async Task<bool> DishExistsById(int id)
		{
			return await repository.AllReadOnly<Dish>().AnyAsync(x => x.Id == id);
		}

		public async Task EditDishAsync(int id, DishFormViewModel model)
		{
			var dish = await repository.All<Dish>().FirstOrDefaultAsync(x => x.Id == id);
            dish.Name = model.Name;
            dish.Price = model.Price;
            await repository.SaveChangesAsync();
		}

		public async Task<DishFormViewModel?> GetDishForEditAsync(int id)
		{           
			return await repository.AllReadOnly<Dish>().Where(x => x.Id == id)
				.Select(x => new DishFormViewModel()
                {
					Id = x.Id,
					Name = x.Name,
					Price = x.Price,
				})
				.FirstOrDefaultAsync();
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

		public async Task<bool> MenuExistsById(int id)
		{
			return await repository.AllReadOnly<Menu>().AnyAsync(x => x.Id == id);
		}
	}
}
