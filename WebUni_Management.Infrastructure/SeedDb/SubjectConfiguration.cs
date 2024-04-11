using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
           .HasMany(sub => sub.Students)
           .WithMany(s => s.Subjects);

            var data = new SeedData();

            builder.HasData(new Subject[] {data.Subject1, data.Subject2, data.Subject3});
        }
    }
}
