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

        public async Task<IEnumerable<MenuItemsViewModel>> AllMenuItemsAsync()
        {
            return await repository.AllReadOnly<Dish>()
                .Select(x => new MenuItemsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price.ToString(),
                    Category = x.Category,
                }).ToListAsync();
        }
    }
}
