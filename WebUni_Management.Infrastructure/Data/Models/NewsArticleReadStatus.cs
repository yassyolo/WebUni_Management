using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("News article read status entity")]
    public class NewsArticleReadStatus
    {
        [Comment("News article identifier")]
        public int NewsArticleId { get; set; }

        [Comment("News article")]
        [ForeignKey(nameof(NewsArticleId))]
        public NewsArticle NewsArticle { get; set; } = null!;

        [Comment("User identifier")]
        public string ReaderId { get; set; } = string.Empty;

        [Comment("User")]
        [ForeignKey(nameof(ReaderId))]
        public ApplicationUser Reader { get; set; } = null!;

        [Comment("Read status")]
        public bool Read { get; set; }
    }
}
