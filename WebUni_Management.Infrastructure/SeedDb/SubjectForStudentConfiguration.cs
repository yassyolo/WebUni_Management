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
    public class SubjectForStudentConfiguration : IEntityTypeConfiguration<SubjectForStudent>
    {
        public void Configure(EntityTypeBuilder<SubjectForStudent> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.SubjectId });
            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Subject)
                .WithMany()
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            var data = new SeedData();
            builder.HasData(new SubjectForStudent[] {data.Subject1ForStudent1, data.Subject2ForStudent1, data.Subject3ForStudent1});
        }
    }
}
