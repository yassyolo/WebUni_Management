using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Canteen;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Canteen dish entity")]
    public class Dish
    {
        [Key]
        [Comment("Dish identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DishNameMaxLength)]
        [Comment("Dish name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DishCategoryMaxLength)]
        [Comment("Dish category")]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(typeof(decimal), DishPriceMinValue, DishPriceMaxValue, ConvertValueInInvariantCulture = false)]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Dish price")]
        public decimal Price { get; set; }

        [Comment("Menu identifier")]
        public int MenuId { get; set; }

        [Comment("Daily menu")]
        public Menu Menu { get; set; } = null!;
    }
}
