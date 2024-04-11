using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class NewsArticleReadStatusConfiguration : IEntityTypeConfiguration<NewsArticleReadStatus>
    {
        public void Configure(EntityTypeBuilder<NewsArticleReadStatus> builder)
        {
            builder.HasKey(x => new { x.NewsArticleId, x.ReaderId });

            var data = new SeedData();

            builder.HasData(new NewsArticleReadStatus[] { data.NewsArticle1ReadStatus, data.NewsArticle2ReadStatus });
        }
    }
}
