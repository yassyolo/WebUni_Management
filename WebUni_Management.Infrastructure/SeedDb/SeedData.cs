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
        public Admin Admin { get; set; }

        public SeedData()
        {
            SeedUsers();
            SeedAdmin();
        }

        private void SeedUsers()
        {
            //string newGuid = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser
            {
                Id = "b242640e-291a-4de7-9701-e3e8e0afb0c9",
                UserName = "00001234",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                IsApproved = true,
            };

            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "admin123");
        }
        private void SeedAdmin()
        {
            Admin= new Admin
            {
                Id = 1,
                UserId = AdminUser.Id
            };
        }

        
    }
}
