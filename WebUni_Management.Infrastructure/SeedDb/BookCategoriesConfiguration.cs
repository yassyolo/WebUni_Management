using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class BookCategoriesConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            var data = new SeedData();

            builder.HasData(new BookCategory[] {data.MathemathicsCategory, data.PhysicsCategory, data.ChemistryCategory});
        }
    }
}
