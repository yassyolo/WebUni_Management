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

        public SeedData()
        {
            SeedUsers();
            SeedAdmin();
            SeedStudent();
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


    }
}
