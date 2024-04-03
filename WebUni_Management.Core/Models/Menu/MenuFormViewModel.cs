using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Canteen;

namespace WebUni_Management.Core.Models.Menu
{
    public class MenuFormViewModel
    { 
        public int MenuId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int DishId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DishNameMaxLength, MinimumLength = DishNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(DishCategoryMaxLength, MinimumLength = DishCategoryMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(typeof(decimal), DishPriceMinValue, DishPriceMaxValue, ConvertValueInInvariantCulture = false, ErrorMessage = MaxLengthErrorMessage)]
        public decimal Price { get; set; }
    }
}
