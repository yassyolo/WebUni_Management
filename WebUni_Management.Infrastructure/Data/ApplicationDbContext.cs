using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.SeedDb;

namespace WebUni_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new AdminsConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<Admin> Admins { get; set; } = null!;
    }
}
