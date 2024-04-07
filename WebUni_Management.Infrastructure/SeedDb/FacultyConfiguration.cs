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
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder
            .HasMany(f => f.Majors)
            .WithOne(m => m.Faculty)
            .OnDelete(DeleteBehavior.Restrict);
            builder
        .HasMany(f => f.Students)
        .WithOne(s => s.Faculty)
        .OnDelete(DeleteBehavior.Restrict);
            var data= new SeedData();
            builder.HasData(new Faculty[] { data.Faculty1 });
        }
    }
}
