using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.NewsArticle;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("News article entity")]
    public class NewsArticle
    {
        [Key]
        [Comment("News article identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NewsTitleMaxLength)]
        [Comment("News article title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(NewsContentMaxLength)]
        [Comment("News article content text")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(NewsImageUrlMaxLength)]
        [Comment("News article image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("News article published on")]
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }
    }
}
