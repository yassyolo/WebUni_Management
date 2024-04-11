using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;


namespace WebUni_Management.Infrastructure.SeedDb
{
	public class NewsArticleConfiguration : IEntityTypeConfiguration<NewsArticle>
    {
        public void Configure(EntityTypeBuilder<NewsArticle> builder)
        {
            builder.HasOne(x => x.Author)
                .WithMany(x => x.NewsArticles)
                .HasForeignKey(x => x.AuthorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

           var data = new SeedData();

            builder.HasData(new NewsArticle[] { data.NewsArticle1, data.NewsArticle2, data.NewsArticle3, data.NewsArticle4 });
        }
    }
}
