using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.NewsArticle;

namespace WebUni_Management.Core.Models.News
{
    public class NewsFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NewsTitleMaxLength, MinimumLength = NewsTitleMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NewsContentMaxLength, MinimumLength = NewsContentMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NewsImageUrlMaxLength, MinimumLength = NewsImageUrlMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Date)]
        public string PublishedOn { get; set; } = string.Empty;

		
		public string Author { get; set; } = string.Empty;
	}
}
