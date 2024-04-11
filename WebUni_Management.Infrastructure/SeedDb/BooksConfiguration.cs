using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
			builder
		    .HasOne(b => b.Renter) 
			.WithMany()    
			.HasForeignKey(b => b.RenterId)
			.IsRequired(false);

			builder
            .HasOne(x => x.Library)
           .WithMany(x => x.Books)
           .HasForeignKey(x => x.LibraryId);

            builder
            .Property(x => x.LibraryId)
            .HasDefaultValue(1);

            var data = new SeedData();

            builder.HasData(new Book[] { data.MathBook1, data.MathBook2, data.MathBook3, data.ChemistryBook, data.PhysicsBook });         
        }
    }
}
