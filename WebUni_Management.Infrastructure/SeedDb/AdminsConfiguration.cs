using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.SeedDb;

namespace WebUni_Management.Data
{
    public class AdminsConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            var data = new SeedData();

            builder.HasData(new Admin[] { data.Admin});
        }
    }
}