using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Library;

namespace WebUni_Management.Core.Models.Library
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage= RequiredErrorMessage)]
        [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(BookAuthorMaxNameLength, MinimumLength = BookAuthorsMinNameLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage= RequiredErrorMessage)]
        [StringLength(BookImageUrlMaxLength, MinimumLength = BookImageUrlMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage= RequiredErrorMessage)]
        [StringLength(BookPublishYearMaxLength, ErrorMessage = InvalidPublishYear)]
        public string PublishYear { get; set; } = string.Empty;

        [Required(ErrorMessage= RequiredErrorMessage)]
        [StringLength(BookDescriptionMaxLength, MinimumLength = BookDescriptionMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public IEnumerable<BookCategoryViewModel> Categories { get; set; } = new List<BookCategoryViewModel>();
    }
}
