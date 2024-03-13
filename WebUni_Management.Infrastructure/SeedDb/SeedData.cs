using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
    public class SeedData
    {
        public ApplicationUser AdminUser { get; set; }
        public ApplicationUser StudentUser { get; set; }
        public Admin Admin { get; set; }
        public Student Student { get; set; }
        public IdentityRole AdminRole { get; set; }
        public IdentityRole StudentRole { get; set; }
        public IdentityUserRole<string> AdminUserRole { get; set; }
        public IdentityUserRole<string> StudentUserRole { get; set; }


        public SeedData()
        {
            SeedUsers();
            SeedAdmin();
            SeedStudent();
            SeedUserRoles();
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser
            {
                Id = "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                UserName = "00000001",
                NormalizedUserName = "00000001",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                IsApproved = true,
            };

            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "admin123");

            StudentUser = new ApplicationUser
            {
                Id = "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                UserName = "12345678",
                NormalizedUserName = "12345678",
                Email = "student@gmail.com",
                NormalizedEmail = "STUDENT@GMAIL.COM",
                EmailConfirmed = true,
                IsApproved= true
            };
            StudentUser.PasswordHash = hasher.HashPassword(StudentUser, "student123");
        }
        private void SeedAdmin()
        {
            Admin= new Admin
            {
                Id = 1,
                UserId = AdminUser.Id
                
            };
        }
        private void SeedStudent()
        {
            Student = new Student()
            { 
                Id = 1,
                FirstName = "Yoana",
                LastName = "Yotova",
                Age = 20,
                PhoneNumber = "0890320424",
                FacultyNumber = "12345678",
                UserId = StudentUser.Id,
            };
        }

        /*private void SeedRoles()
        {
            AdminRole = new IdentityRole
            {
                Id = "02853dfe-8461-47a5-b545-8aab884099a3",
                Name = "Admin",
                NormalizedName = "ADMINISTRATOR".ToUpper()
            };

            StudentRole = new IdentityRole
            {
                Id = "25b7786d-75f0-42a0-94a5-64eef4ca93a6",
                Name = "Student",
                NormalizedName = "STUDENT".ToUpper()
            };
        }*/
        private void SeedUserRoles()
        {
            AdminUserRole = new IdentityUserRole<string>
            {
                UserId = AdminUser.Id,
                RoleId = "02853dfe-8461-47a5-b545-8aab884099a3" // AdminRole ID
            };

            StudentUserRole = new IdentityUserRole<string>
            {
                UserId = StudentUser.Id,
                RoleId = "25b7786d-75f0-42a0-94a5-64eef4ca93a6" // StudentRole ID
            };
        }
    }
}

  

