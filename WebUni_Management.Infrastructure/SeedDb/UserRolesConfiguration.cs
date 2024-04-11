using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var data = new SeedData();

            builder.HasData(new IdentityUserRole<string>[] { data.AdminUserRole, data.StudentUserRole });
        }
    }
}
