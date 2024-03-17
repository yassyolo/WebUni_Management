using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.SeedDb;

namespace WebUni_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
            )
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new AdminsConfiguration());
            builder.ApplyConfiguration(new StudentsConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());
            builder.ApplyConfiguration(new LibraryConfiguration());
            builder.ApplyConfiguration(new BookCategoriesConfiguration());
            builder.ApplyConfiguration(new BooksConfiguration());
            builder.ApplyConfiguration(new BookAuthorsConfiguration());
            builder.ApplyConfiguration(new BooksByBookAuthorConfiguration());

            
            base.OnModelCreating(builder);
        }

        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookCategory> BookCategories { get; set; } = null!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public DbSet<BookByBookAuthor> BookByBookAuthors { get; set; } = null!;
        public DbSet<Library> Library { get; set; } = null!;
    }
}
