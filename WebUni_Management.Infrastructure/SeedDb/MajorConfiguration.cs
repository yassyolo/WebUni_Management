using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class MajorConfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder
            .HasMany(m => m.CourseTerms)
            .WithOne(ct => ct.Major)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasMany(m => m.Students)
            .WithOne(s => s.Major)
            .OnDelete(DeleteBehavior.Restrict);

            var data = new SeedData();

            builder.HasData(new Major[] { data.Major1 });
        }
    }
}
