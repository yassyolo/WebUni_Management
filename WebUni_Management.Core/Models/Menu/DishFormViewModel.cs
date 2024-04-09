using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Canteen;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
namespace WebUni_Management.Core.Models.Menu
{
	public class DishFormViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(DishNameMaxLength, MinimumLength = DishNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(typeof(decimal), DishPriceMinValue, DishPriceMaxValue, ConvertValueInInvariantCulture = false, ErrorMessage = InvalidPrice)]
		[Column(TypeName = "decimal(18,2)")]
		[Comment("Dish price")]
		public decimal Price { get; set; }
	}
}
