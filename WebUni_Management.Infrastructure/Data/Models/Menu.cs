using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Menu entity")]
    public class Menu
    {
        [Key]
        [Comment("Canteen identifier")]
        public int Id { get; set; }

        [Comment("Canteen dishes")]
        public IEnumerable<Dish> Dishes { get; set; } = new List<Dish>();

        [Required]
        [Comment("Canteen date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
