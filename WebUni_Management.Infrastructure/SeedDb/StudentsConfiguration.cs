using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                    .HasMany(s => s.RentedBooks)
                    .WithOne()
                    .HasForeignKey(book => book.RenterId)
                    .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            
            builder.HasData(new Student[] { data.Student });
        }
    }
}
