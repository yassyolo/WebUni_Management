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
    public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            var data = new SeedData();
            builder.HasData(new BookAuthor[] { data.BookAuthor1, data.BookAuthor2, data.BookAuthor3, data.BookAuthor4, data.BookAuthor5, data.BookAuthor6 });
        }
    }
}
