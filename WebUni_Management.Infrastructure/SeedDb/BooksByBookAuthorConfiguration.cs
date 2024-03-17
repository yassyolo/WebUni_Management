using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
    public class BooksByBookAuthorConfiguration : IEntityTypeConfiguration<BookByBookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookByBookAuthor> builder)
        {
            builder
           .HasKey(bba => new { bba.BookId, bba.AuthorId });
        
           var data = new SeedData();
            builder.HasData(new BookByBookAuthor[] { data.MathBook1ByBookAuthor1, data.MathBook1ByBookAuthor2, data.MathBook2ByBookAuthor3, data.MathBook3ByBookAuthor4, data.ChemistryBookByBookAuthor5, data.PhysicsBookByBookAuthor6});
        }
    }
}
