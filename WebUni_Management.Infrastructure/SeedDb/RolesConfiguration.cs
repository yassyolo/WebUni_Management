using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var data = new SeedData();

            builder.HasData(new IdentityRole[] { data.AdminRole, data.StudentRole });
        }
    }
}
