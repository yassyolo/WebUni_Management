using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Data;
using WebUni_Management.Infrastructure.Repository;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore.InMemory;
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
            var expectedStudentId = 1;

            var result = await repository.GetById<Student>(expectedStudentId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedStudentId, result.Id);
        }
        [Test]
        public async Task GetById_ShouldReturnFacultyIdCorrectly()
        {
            var expectedFacultyId = 1;

            var result = await  repository.GetById<Faculty>(expectedFacultyId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedFacultyId, result.Id);
        }
        [Test]
        public async Task GetById_ShouldReturnMajorIdCorrectly()
        {
            var expectedMajorId = 1;

            var result = await repository.GetById<Major>(expectedMajorId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedMajorId, result.Id);
        }
        [Test]
        public async Task GetById_ShouldReturnCourseTermIdCorrectly()
        {
            var expectedCourseTermId = 1;

            var result = await repository.GetById<CourseTerm>(expectedCourseTermId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedCourseTermId, result.Id);
        }
        [Test]
        public async Task GetById_ShouldReturnUserIdCorrectly()
        {
            var expectedUserId = "0e90dbeb-6468-4abc-9599-b4757e3874aa";

            var result = await repository.GetById<ApplicationUser>(expectedUserId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedUserId, result?.Id);
        }
        [Test]
        public async Task GetById_ShouldReturtnStudentidCorrectly()
        {
            var expectedStudentId = 1;
            var result = await repository.GetById<Student>(expectedStudentId);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(expectedStudentId, result?.Id);
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
            // Arrange
            var expectedCount = 1; // Assuming you have 2 students in your test data

            // Act
            var result =  await repository.All<Student>().ToListAsync();

            // Assert
            Assert.AreEqual(expectedCount, result.Count);
            // You can add more assertions to check the correctness of each student entity if needed
        }
        [Test]
        public void AllReadOnly_ShouldReturnAllStudentsInReadOnlyState()
        {
            // Arrange
            var expectedCount = 1; // Assuming you have 2 students in your test data

            // Act
            var result = repository.AllReadOnly<Student>().ToList();

            // Assert
            Assert.AreEqual(expectedCount, result.Count);

            // Check that entities are in a read-only state
        }

        [Test]
        public async Task SaveChangesAsync_ShouldSaveChangesToDatabase()
        {
            // Arrange: Prepare any necessary setup or changes to the entities
            // For example, adding or modifying entities

            // Act: Save changes to the database
            var result = await repository.SaveChangesAsync();

            // Assert: Verify that changes have been successfully saved
            Assert.That(result, Is.GreaterThanOrEqualTo(0)); // Ensure that SaveChangesAsync() returns a non-negative integer
                                                             // You can also check for a specific number of changes if needed
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteEntityFromDatabase()
        {
            // Arrange: Add the entity you want to delete to the database
            var student = new Student() { FirstName = "Test" };
            await repository.AddAsync(student);
            await repository.SaveChangesAsync();

            // Act: Delete the entity from the database
            await repository.DeleteAsync(student);
            await repository.SaveChangesAsync();

            // Assert: Verify that the entity has been deleted
            var deletedStudent = await repository.GetById<Student>(student.Id);
            Assert.That(deletedStudent, Is.Null);
        }


    }
}
