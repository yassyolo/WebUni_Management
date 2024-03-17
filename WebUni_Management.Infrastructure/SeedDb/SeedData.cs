using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public BookCategory MathemathicsCategory { get; set; }
        public BookCategory PhysicsCategory { get; set; }
        public BookCategory ChemistryCategory { get; set; }
        public Book MathBook1 { get; set; }
        public Book MathBook2 { get; set; }
        public Book MathBook3 { get; set; }
        public Book PhysicsBook { get; set; }
        public Book ChemistryBook { get; set; }
        public Library Library { get; set; }
        public BookAuthor BookAuthor1 { get; set; }
        public BookAuthor BookAuthor2 { get; set; }
        public BookAuthor BookAuthor3 { get; set; }
        public BookAuthor BookAuthor4 { get; set; }
        public BookAuthor BookAuthor5 { get; set; }
        public BookAuthor BookAuthor6 { get; set; }
        public BookByBookAuthor MathBook1ByBookAuthor1 { get; set; }
        public BookByBookAuthor MathBook1ByBookAuthor2 { get; set; }
        public BookByBookAuthor MathBook2ByBookAuthor3 { get; set; }
        public BookByBookAuthor MathBook3ByBookAuthor4 { get; set; }
        public BookByBookAuthor ChemistryBookByBookAuthor5 { get; set; }
        public BookByBookAuthor PhysicsBookByBookAuthor6 { get; set; }
        public SeedData()
        {
            SeedUsers();
            SeedAdmin();
            SeedStudent();
            SeedUserRoles();
            SeedBookCategories();
            SeedLibrary();
            SeedBooks();
            SeedBookAuthors();
            SeedBookByBookAuthors();
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
                UserId = StudentUser.Id
            };
        }
        private void SeedUserRoles()
        {
            AdminUserRole = new IdentityUserRole<string>
            {
                UserId = AdminUser.Id,
                RoleId = "02853dfe-8461-47a5-b545-8aab884099a3" 
            };

            StudentUserRole = new IdentityUserRole<string>
            {
                UserId = StudentUser.Id,
                RoleId = "25b7786d-75f0-42a0-94a5-64eef4ca93a6" 
            };
        }
        private void SeedBookCategories()
        {
            MathemathicsCategory = new BookCategory
            {
                Id = 1,
                Name = "Mathematics",
            };
            PhysicsCategory = new BookCategory
            {
                Id = 2,
                Name = "Physics",
            };
            ChemistryCategory = new BookCategory
            {
                Id = 3,
                Name = "Chemistry",
            };
        }
        private void SeedBookAuthors()
        {
            BookAuthor1 = new BookAuthor
            {
                Id = 1,
                FirstName = "Natasha",
                LastName = "Maurits",
                //Books = new List<Book> { MathBook1 }
            };
            BookAuthor2 = new BookAuthor
            {
                Id = 2,
                FirstName = "Branislava",
                LastName = "Ćurčić-Blake",
            };
            BookAuthor3 = new BookAuthor
            {
                Id = 3,
                FirstName = "Hugh",
                LastName = "Neill",
            };
            BookAuthor4 = new BookAuthor
            {
                Id = 4,
                FirstName = "Kingsley",
                LastName = "Augustine",
            };
            BookAuthor5 = new BookAuthor
            {
                Id = 5,
                FirstName = "Ramesh",
                LastName = "Chandra",
            };
            BookAuthor6 = new BookAuthor
            {
                Id = 6,
                FirstName = "Mani",
                LastName = "Naidu",
            };
        }

        private void SeedLibrary()
        {
            Library = new Library
            {
                Id = 1,
            };
        }

        private void SeedBooks()
        {
            MathBook1 = new Book
            {
                Id = 1,
                Title = "Math for Scientists: Refreshing the Essentials",
                ImageUrl = "https://m.media-amazon.com/images/I/617vHgW8ZhL._SY522_.jpg",
                //Author = new List<BookAuthor> {BookAuthor1, BookAuthor2 },
                CategoryId = MathemathicsCategory.Id,
                PublishYear = "2017",
                Description = "'Math for Scientists: Refreshing the Essentials' offers a concise yet comprehensive review of fundamental mathematical concepts essential for scientists.Co - authored by Branislava Ćurčić - Blake and Natalia Maria, this book serves as a valuable resource for refreshing and reinforcing mathematical skills necessary for scientific inquiry.",
                IsRented = true,
                RenterId = Student.Id,
                RentalDate = new DateTime(2024, 2, 16),
                LibraryId = Library.Id
            };
            MathBook2 = new Book
            {
                Id = 2,
                Title = "Mathematics: A Complete Introduction",
                ImageUrl = "https://m.media-amazon.com/images/I/71EUTt1F2vL._SY522_.jpg",
                //Author = new List<BookAuthor> { BookAuthor3 },
                CategoryId = MathemathicsCategory.Id,
                PublishYear = "2018",
                Description = "Master Math effortlessly with this comprehensive guide. Ideal for beginners and intermediates, it features step-by-step explanations, practice questions, and chapter summaries for confident learning. No separate workbooks needed!",
                IsRented = false,
                LibraryId = Library.Id
            };
            MathBook3 = new Book
            {
                Id = 3,
                Title = "Simplified Statistics and Probability: A Mathematics Book for High Schools and Colleges",
                ImageUrl = "https://m.media-amazon.com/images/I/61CANeMV8wL._SY522_.jpg",
                //Author = new List<BookAuthor> { BookAuthor4 },
                CategoryId = MathemathicsCategory.Id,
                PublishYear = "2018",
                Description = "'Simplified Statistics and Probability' is a comprehensive book designed for high school and college students. It offers clear explanations, numerous examples, and practice exercises with answers for self-assessment, enhancing understanding and proficiency in the subject.",
                IsRented = false,
                LibraryId = Library.Id
            };
            ChemistryBook = new Book
            {
                Id = 4,
                Title = "Basic Organic Chemistry",
                ImageUrl = "https://m.media-amazon.com/images/I/813VoAjptdL._SY522_.jpg",
                //Author = new List<BookAuthor> { BookAuthor5 },
                CategoryId = ChemistryCategory.Id,
                PublishYear = "2019",
                Description = "'Basic Organic Chemistry' covers fundamental concepts, organic molecules, functional groups, nomenclature, acids/bases, stereochemistry, amino acids, proteins, carbohydrates, alcohols, ethers, and spectroscopy, offering insights for understanding organic reactions.",
                IsRented = false,
                LibraryId = Library.Id
            };
            PhysicsBook = new Book
            {
                Id = 5,
                Title = "Engineering Physics",
                ImageUrl = "https://m.media-amazon.com/images/I/81p+3Q5hsvL._SY522_.jpg",
                //Author = new List<BookAuthor> { BookAuthor6 },
                CategoryId = PhysicsCategory.Id,
                PublishYear = "2010",
                Description = "'Engineering Physics' caters to first-year undergraduates at Jawaharlal Nehru Technical University. Covering crystallography, quantum mechanics, metals, dielectrics, semiconductors, superconductivity, lasers, holography, nanotechnology, and optics, it employs clear pedagogy for comprehensive learning.",
                IsRented = false,
                LibraryId = Library.Id
            };
        }

        private void SeedBookByBookAuthors()
        {
            MathBook1ByBookAuthor1 = new BookByBookAuthor
            {
                BookId = MathBook1.Id,
                AuthorId = BookAuthor1.Id
            };
            MathBook1ByBookAuthor2 = new BookByBookAuthor
            {
                BookId = MathBook1.Id,
                AuthorId = BookAuthor2.Id
            };
            MathBook2ByBookAuthor3 = new BookByBookAuthor
            {
                BookId = MathBook2.Id,
                AuthorId = BookAuthor3.Id
            };
            MathBook3ByBookAuthor4 = new BookByBookAuthor
            {
                BookId = MathBook3.Id,
                AuthorId = BookAuthor4.Id
            };
            ChemistryBookByBookAuthor5 = new BookByBookAuthor
            {
                BookId = ChemistryBook.Id,
                AuthorId = BookAuthor5.Id
            };
            PhysicsBookByBookAuthor6 = new BookByBookAuthor
            {
                BookId = PhysicsBook.Id,
                AuthorId = BookAuthor6.Id
            };
        }

    }
}

  

