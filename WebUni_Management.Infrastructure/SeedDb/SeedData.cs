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
        public StudyRoom BigStudyRoom { get; set; }
        public StudyRoom SmallStudyRoom { get; set; }
        public StudyRoom MediumStudyRoom { get; set; }
        public StudyRoom SingleStudyRoom { get; set; }
        public Dish Salad1 { get; set; }
        public Dish Salad2 { get; set; }
        public Dish MainDish1 { get; set; }
        public Dish MainDish2 { get; set; }
        public Dish Dessert1 { get; set; }
        public Dish Dessert2 { get; set; }
        public Menu Menu { get; set; }
        public NewsArticle NewsArticle1 { get; set; }
        public NewsArticle NewsArticle2 { get; set; }
        public NewsArticle NewsArticle3 { get; set; }
        public NewsArticle NewsArticle4 { get; set; }
        public NewsArticleReadStatus NewsArticle1ReadStatus { get; set; }
        public NewsArticleReadStatus NewsArticle2ReadStatus { get; set; }
        public Event Event1 { get; set; }
        public Event Event2 { get; set; }
        public Event Event3 { get; set; }
        public EventParticipant EventParticipant1 { get; set; }

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
            SeedStudyRooms();
            SeedMenu();
            SeedDishes();
            SeedNewsArticles();
            SeedNewsArticleReadStatus();
            SeedEvents();
            SeedEventParticipants();
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
                RenterId = StudentUser.Id,
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

        private void SeedStudyRooms()
        {
            SmallStudyRoom = new StudyRoom
            {
                Id = 1,
                Name = "Cozy Study Room for Three, a Heaven for Productivity",
                Description = "Comfortable, productive space for focused work & collaboration. Equipped with modern amenities to support efficient work sessions. To enhance concentration, the room is designed with sound-absorbing materials to minimize distractions from outside noise.",
                Capacity = 3,
                IsRented = false,
                LibraryId = Library.Id,
                Floor = 1,
                ImageUrl = "https://st.hzcdn.com/simgs/pictures/home-offices/calender-allen-architecture-llc-img~cd81328d0bb27f02_8-0752-1-86bb54d.jpg"
            };
            MediumStudyRoom = new StudyRoom
            {
                Id = 2,
                Name = "Study Nook, space for 5, fostering productivity and creativity",
                Description = "Discover a Serene Study Haven: Our spacious room comfortably accommodates up to 5 people, offering ergonomic seating, ample desk space, and abundant natural light to foster productivity and concentration. Delight in the quiet ambiance and conducive environment for collaborative projects, group discussions, or solitary study sessions. Elevate your learning experience in this peaceful retreat designed for academic excellence and intellectual pursuits.",
                Capacity = 5,
                IsRented = false,
                LibraryId = Library.Id,
                Floor = 2,
                ImageUrl = "https://st.hzcdn.com/simgs/pictures/home-offices/white-and-airy-jennifer-pacca-interiors-img~387173790a9ee6b8_8-4497-1-a0376b0.jpg"
            };
           BigStudyRoom = new StudyRoom
            {
                Id = 3,
                Name = "Elite Learning Oasis, The Grand Study Room for 10",
                Description = "Step into our expansive study sanctuary designed to accommodate up to 10 individuals. With abundant space, ergonomic furnishings, and a tranquil atmosphere, this room fosters focused study sessions, collaborative brainstorming, and group projects. Elevate your academic pursuits in this premium environment tailored for productivity and intellectual growth.",
                Capacity = 10,
                IsRented = false,
                LibraryId = Library.Id,
                Floor = 3,
                ImageUrl = "https://st.hzcdn.com/simgs/pictures/home-offices/eclectic-and-colorful-greensboro-nc-jessica-dauray-interiors-elements-of-style-img~362195d10a0dcf59_8-0725-1-24dcf75.jpg"
           };
            SingleStudyRoom = new StudyRoom
            {
                Id = 4,
                Name = "Solitude Haven, Private Study Retreat",
                Description = "Escape to your own secluded sanctuary for uninterrupted focus and productivity. Our single study room, designed for one individual, offers a tranquil environment with ergonomic furnishings and ample natural light. Dive into your studies, research, or creative projects in complete privacy, free from distractions. Maximize your productivity and achieve your academic or professional goals in this serene haven tailored just for you.",
                Capacity = 1,
                IsRented = true,
                RenterId = StudentUser.Id,
                RentalDate = new DateTime(2024, 2, 16),
                LibraryId = Library.Id,
                Floor = 1,
                ImageUrl = "https://st.hzcdn.com/simgs/pictures/home-offices/contemporary-home-office-tazz-lighting-inc-img~259113440b895f26_8-5371-1-da12f0e.jpg"
            };
        }
        private void SeedMenu()
        {
            Menu = new Menu
            {
                Id = 1,
                Date = new DateTime(2024, 2, 20)
            };
        }
        private void SeedDishes()
        {
            Salad1 = new Dish
            {
                Id = 1,
                Name = "Greek Salad",
                Category = "Salad",
                Price = 1.00m,
                MenuId = Menu.Id
            };
            Salad2 = new Dish
            {
                Id = 2,
                Name = "Caesar Salad",
                Category = "Salad",
                Price = 1.50m,
                MenuId = Menu.Id
            };
            MainDish1 = new Dish
            {
                Id = 3,
                Name = "Spaghetti Carbonara",
                Category = "Main Dish",
                Price = 2.00m,
                MenuId = Menu.Id
            };
            MainDish2 = new Dish
            {
                Id = 4,
                Name = "Chicken Alfredo",
                Category = "Main Dish",
                Price = 2.50m,
                MenuId = Menu.Id
            };
            Dessert1 = new Dish
            {
                Id = 5,
                Name = "Tiramisu",
                Category = "Dessert",
                Price = 1.00m,
                MenuId = Menu.Id
            };
            Dessert2 = new Dish
            {
                Id = 6,
                Name = "Cheesecake",
                Category = "Dessert",
                Price = 1.50m,
                MenuId = Menu.Id
            };
        }
        private void SeedNewsArticles()
        {
            NewsArticle1 = new NewsArticle
            {
                Id = 1,
                Title = "New Study Term Begins: Students Prepare for Month-Long Exams",
                Content = "With the start of a new study term, students are gearing up for a month-long examination period. Across educational institutions, there's a mix of anticipation and determination as learners of all ages prepare to showcase their knowledge.\r\n\r\nProfessors are finalizing exam schedules, ensuring students are ready for comprehensive assessments. Students are employing various study techniques, from late-night sessions to group study, to maximize their performance.\r\n\r\nThroughout the month, students will face challenges, but with determination, they're poised to excel. It's a time of growth and resilience as students strive for success in their academic endeavors.",
                PublishedOn = new DateTime(2024, 3, 24),
                ImageUrl = "https://www.inspiringinterns.com/blog/wp-content/uploads/2017/05/time-481447-1200x849.jpg",
                AuthorId = Student.Id,
                IsApproved = true
            };
            NewsArticle2 = new NewsArticle
            {
                Id = 2,
                Title = "Local Programming Firm Sponsors Computer Facility, Elevating Education Standards",
                Content = "In a significant stride towards enhancing educational opportunities, a local programming firm has stepped forward to sponsor a state-of-the-art computer facility. This initiative aims to empower students with access to cutting-edge technology and resources, thereby enriching their learning experience.\r\n\r\nWith the new facility, students can delve deeper into computer science and technology, exploring programming languages, software development, and digital literacy. The firm's sponsorship ensures that students receive a quality education, equipping them with essential skills for the modern workforce.\r\n\r\nThrough this partnership, the community sees the fusion of education and industry, paving the way for innovation and growth. It's a testament to the firm's commitment to nurturing talent and fostering development at the grassroots level.",
                PublishedOn = new DateTime(2024, 3, 23),
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a5/Contemporary_Computer_Lab.jpg/1920px-Contemporary_Computer_Lab.jpg"
            };
            NewsArticle3 = new NewsArticle
            {
                Id = 3,
                Title = "Student Board Celebrates Dimitar's Name Day with Homemade Banitza",
                Content = "In a heartwarming gesture to celebrate Dimitar's Name Day, the student board organized a delightful event filled with the aroma of freshly baked banitza. As part of the tradition, the student board members meticulously prepared homemade banitza, to honor Dimitar and bring joy to the campus community. The event, held in the university courtyard, attracted a crowd eager to indulge in this cherished delicacy. With laughter, students savored each bite of the warm banitza, symbolizing the spirit of togetherness and cultural pride. Amidst the festivities, professors expressed gratitude to the student board for their thoughtful gesture, emphasizing the importance of preserving cultural traditions within the university.",
                PublishedOn = new DateTime(2023, 10, 26),
                ImageUrl = "https://www.nakotlona.bg/wp-content/uploads/2021/01/-%D0%B1%D0%B0%D0%BD%D0%B8%D1%86%D0%B0-e1609604417802.jpg"
            };
            NewsArticle4 = new NewsArticle
            {
                Id = 4,
                Title = "Programming Firms Open Doors to Students: Explore Opportunities and Get Career Advice",
                Content = "Programming firms are opening their doors to students, offering an unique opportunity to explore career possibilities and gain valuable insights into the tech industry. The \"Day of Open Doors\" initiative aims to bridge the gap between academia and industry, allowing students to interact with professionals, showcase their skills, and learn about job opportunities.\r\n\r\nDuring these events, students are encouraged to bring their CVs and portfolios, ready to engage with recruiters and hiring managers. From software development to project management, companies present a range of roles available, catering to various skill levels and interests within the field of technology.\r\n\r\nIn addition to networking and recruitment opportunities, students can participate in workshops, panel discussions, and tech demos, gaining firsthand knowledge about the latest trends and technologies shaping the industry. Seasoned professionals are on hand to offer advice, mentorship, and career guidance, empowering students to make informed decisions about their future careers.\r\n\r\nThe \"Day of Open Doors\" serves as a platform for collaboration and knowledge exchange, fostering connections between aspiring technologists and industry leaders.",
                PublishedOn = new DateTime(2024, 1, 13),
                ImageUrl = "https://www.asid.org/img/cache/events/Events/6964/lead_image/145229-720-405f-0dee4087947fd39207a5bb29630aac0b"
            };
        }

        private void SeedNewsArticleReadStatus()
        {
            NewsArticle1ReadStatus = new NewsArticleReadStatus
            {
                NewsArticleId = NewsArticle1.Id,
                ReaderId = StudentUser.Id,
                Read = true
            };
            NewsArticle2ReadStatus = new NewsArticleReadStatus
            {
                NewsArticleId = NewsArticle2.Id,
                ReaderId = StudentUser.Id,
                Read = true
            };
        }

        private void SeedEvents()
        {
            Event1 = new Event
            {
                Id = 1,
                Name = "Exploring the Power of C#",
                Description = "Embark on a journey into the realm of software development with our captivating seminar on \"Why Choose C#.\" Led by a seasoned industry expert, this seminar delves deep into the myriad benefits and advantages of utilizing C# as your programming language of choice. From its robust object-oriented design to its versatility in application development, C# offers unparalleled opportunities for both novice and seasoned developers alike. Join us as we uncover the power and potential of C#, and discover why it remains a top choice in the ever-evolving landscape of technology. Reserve your seat now for an enlightening experience you won't want to miss!",
                StartTime = new DateTime(2024, 4, 15, 14, 0, 0),
                EndTime = new DateTime(2024, 4, 15, 16, 0, 0),
                ImageUrl = "https://ardounco.sirv.com/WP_content.bytehide.com/2022/03/why-learn-csharp.png",
                GuestParticipant = "John Doe",
                Capacity = 50,
            };
            Event2 = new Event
            {
                Id = 2,
                Name = "Exploring Embedded Technologies and IoT Seminar",
                Description = "Join me for an insightful seminar where I delve into the fascinating world of embedded technologies and the Internet of Things (IoT). Discover how embedded systems are revolutionizing various industries, from smart homes to industrial automation. Learn about the latest trends, challenges, and opportunities in the realm of IoT, and explore real-world applications that are shaping the future of technology. Whether you're a seasoned engineer or an enthusiast curious about the possibilities of connected devices, this seminar promises to expand your knowledge and inspire innovation.",
                StartTime = new DateTime(2024, 5, 2, 15, 0, 0),
                EndTime = new DateTime(2024, 5, 2, 18, 0, 0),
                ImageUrl = "https://builtin.com/sites/www.builtin.com/files/styles/og/public/2022-08/connected-devices-internet-of-things-iot-devices.png",
                GuestParticipant = "Jane Dimova",
                Capacity = 65,
            };
            Event3 = new Event
            {
                Id = 3,
                Name = "Unveiling the Role of QA Tester: Ensuring Quality in Software Development",
                Description = "Join us for an illuminating seminar as we explore the profession of QA (Quality Assurance) Tester and its pivotal role in ensuring the quality of software products. Gain insights into the responsibilities, methodologies, and best practices employed by QA testers to identify bugs, verify functionality, and enhance user experience. Discover how QA testing contributes to the success of software projects by mitigating risks and improving product reliability. Whether you're an aspiring QA tester or simply curious about the behind-the-scenes of software development, this seminar offers a comprehensive overview of the QA profession and its significance in delivering high-quality software.",
                StartTime = new DateTime(2024, 3, 30, 11, 0, 0),
                EndTime = new DateTime(2024, 3, 30, 15, 0, 0),
                ImageUrl = "https://testpro.io/wp-content/uploads/2023/11/qa-tester.jpg",
                GuestParticipant = "Boris Cholakov",
                Capacity = 30,
            };
        }

        private void SeedEventParticipants()
        {
            EventParticipant1 = new EventParticipant
            {
                EventId = Event1.Id,
                ParticipantId = StudentUser.Id
            };
        }
    }
}

  

