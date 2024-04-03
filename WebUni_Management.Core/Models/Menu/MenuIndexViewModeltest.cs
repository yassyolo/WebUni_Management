using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Menu
{
    public class MenuIndexViewModeltest
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;

        public IEnumerable<MenuItemsViewModel> Dishes = new List<MenuItemsViewModel>();
    }
}
