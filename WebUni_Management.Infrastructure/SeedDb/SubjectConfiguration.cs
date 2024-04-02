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
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            var data = new SeedData();
            builder.HasData(new Subject[] {data.Subject1, data.Subject2, data.Subject3});
        }
    }
}
