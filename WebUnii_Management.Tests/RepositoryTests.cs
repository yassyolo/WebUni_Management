using Microsoft.EntityFrameworkCore;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Repository;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;

namespace WebUnii_Management.Tests
{
	[TestFixture]
    public class RepositoryTests
    {
        private ApplicationDbContext context;
        private IRepository repository;

        [SetUp] 
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            SeedData();
           
            repository = new Repository(context);
        }

        private void SeedData()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var studentUser = new ApplicationUser
            {
                Id = "0e90dbeb-6468-4abc-9599-b4757e3874aa",
                UserName = "12345678",
                NormalizedUserName = "12345678",
                Email = "student@gmail.com",
                NormalizedEmail = "STUDENT@GMAIL.COM",
                EmailConfirmed = true,
                IsApproved = true
            };
            context.Add(studentUser);
            studentUser.PasswordHash = hasher.HashPassword(studentUser, "student123");
            var faculty = new Faculty
            {
                Id = 1,
                Name = "Mathematics",
                Description = "The Mathematics Faculty offers a wide range of courses in pure and applied mathematics, statistics, and computer science. Our faculty members are dedicated to providing students with a solid foundation in mathematical theory and practical skills, preparing them for successful careers in academia, industry, and research.",
            };
            context.Add(faculty);
            var major = new Major
            {
                Id = 1,
                Name = "Computer Science",
                FacultyId = faculty.Id,
                Description = "The Computer Science major equips students with the knowledge and skills needed to excel in the rapidly evolving field of technology. Our comprehensive curriculum covers programming languages, algorithms, data structures, software engineering, and more, preparing students for diverse career opportunities in software development, cybersecurity, artificial intelligence, and beyond."
            };
            context.Add(major);
            var courseTerm = new CourseTerm
            {
                Id = 1,
                Name = "Summer 24",
                MajorId = major.Id
            };
            context.Add(courseTerm);
            var student = new Student()
            {
                Id = 1,
                FirstName = "Yoana",
                LastName = "Yotova",
                Age = 20,
                PhoneNumber = "0890320424",
                FacultyNumber = "12345678",
                UserId = studentUser.Id,
                FacultyId = faculty.Id,
                MajorId = major.Id,
                CourseTermId = courseTerm.Id
            };
            context.Add(student);
            context.SaveChangesAsync();
        }

        [Test]
        public async Task GetById_ShouldReturnStudentIdCorrectly()
        {
            var result = await repository.GetById<Student>(1);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(1, result.Id);
        }


        [Test]
        public async Task Add_ShouldAddStudentCorrectly()
        {
            var student = new Student() { FirstName = "Test" };
            var existingStudent = await repository.GetById<Student>(student.Id);
            if (existingStudent != null)
            {
                context.Entry(existingStudent).State = EntityState.Detached;
            }

            await repository.AddAsync(student);
            await repository.SaveChangesAsync();

            var result = await repository.GetById<Student>(student.Id);

            Assert.AreEqual(student.FirstName, result.FirstName);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Student>());
        }

        [Test]
        public async Task All_ShouldReturnAllStudents()
        {
            var result =  await repository.All<Student>().ToListAsync();

            Assert.AreEqual(1, result.Count);
        }
        [Test]
        public void AllReadOnly_ShouldReturnAllStudentsInReadOnlyState()
        {
            var result = repository.AllReadOnly<Student>().ToList();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task SaveChangesAsync_ShouldSaveChangesToDatabase()
        {
            var result = await repository.SaveChangesAsync();

            Assert.That(result, Is.GreaterThanOrEqualTo(0));                                                           
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteEntityFromDatabase()
        {
            await repository.AddAsync(new Student() 
            { 
                Id = 10,
                FirstName = "Test" 
            });

            var student = await repository.GetById<Student>(10);
            await repository.DeleteAsync(student);

            var deletedStudent = await repository.GetById<Student>(10);
            Assert.That(deletedStudent, Is.Null);
        }
		[TearDown]
		public void TearDown()
		{
			context.Database.EnsureDeleted();
		}

	}
}
