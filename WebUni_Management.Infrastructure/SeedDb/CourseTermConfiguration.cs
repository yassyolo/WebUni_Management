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
    public class CourseTermConfiguration : IEntityTypeConfiguration<CourseTerm>
    {
        public void Configure(EntityTypeBuilder<CourseTerm> builder)
        {
            builder
        .HasMany(ct => ct.Students)
        .WithOne(s => s.CourseTerm)
        .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            builder.HasData(new CourseTerm[] { data.CourseTerm1 });
        }
    }
}
