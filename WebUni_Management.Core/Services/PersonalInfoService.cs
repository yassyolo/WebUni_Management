using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Library;
using WebUni_Management.Core.Models.PersonalInfo;
using WebUni_Management.Infrastructure.Data.Models;
using WebUni_Management.Infrastructure.Repository;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants;

namespace WebUni_Management.Core.Services
{
    public class PersonalInfoService : IPersonalInfoService
	{
		private readonly IRepository repository;

		public PersonalInfoService(IRepository _repository)
		{
			repository = _repository;
		}

        public async Task<SearchStudentViewModel> FilterStudentAsync(string? searchTerm = null)
        {
            var student = repository.AllReadOnly<Infrastructure.Data.Models.Student>();

            if (searchTerm == null && string.IsNullOrWhiteSpace(searchTerm)) 
            {
                return null;
            }
            var normalizedSearchTerm = searchTerm.ToLower();
            student = student.Where(x => x.FacultyNumber.Contains(normalizedSearchTerm));

            var studentToShow = await student.Select(x => new StudentIndexViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FacultyNumber = x.FacultyNumber,
                Email = x.User.Email
            }).FirstOrDefaultAsync();
            return new SearchStudentViewModel()
            {
                Student = studentToShow
            };
        }

        public async Task<StudentDetailsViewModel?> GetStudentDetailsByIdAsync(int id)
        {
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == id)
                .Select( x => new StudentDetailsViewModel()
                {
                    Id= x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    PhoneNumber = x.PhoneNumber,
                    FacultyNumber= x.FacultyNumber,
                    Email = x.User.Email,
                    RentedBooksCount = x.RentedBooks.Count(),
                    RentedStudyRoomsCount = x.RentedStudyRooms.Count(),
                })
                .FirstOrDefaultAsync();
            var studentId = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == id).Select(x => x.UserId).FirstOrDefaultAsync();
            student.Subjects = await GetSubjects(studentId);
            
            return student;
        }

		private async Task<IEnumerable<SubjectIndexViewModel>> GetSubjects(string userId)
		{
			return await repository.AllReadOnly<SubjectForStudent>().Where(x => x.StudentId == userId)
				.Select(x => new SubjectIndexViewModel()
				{
					Id = x.Subject.Id,
					Name = x.Subject.Name,
				}).ToListAsync();
		}

		public async Task<IEnumerable<BookInfoViewModel>> MyRentedBooksAsync(string userId)
        {
            return await repository.AllReadOnly<Book>()
                .Where(x => x.RenterId == userId)
                .Select(x => new BookInfoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = string.Join(", ", x.Author.Select(x => x.FirstName + " " + x.LastName)),
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.Name,
                }).ToListAsync();
        }

        public async Task RemoveBookRentAsync(int id, string userId)
        {
            var book =  repository.All<Book>().FirstOrDefault(x => x.Id == id && x.RenterId == userId);
            book.RenterId = null;
            book.IsRented = false;
            book.RentalDate = null;
            await repository.SaveChangesAsync();
        }

        public async Task<bool> RentedBookExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Book>().Where(x => x.Id == id).AnyAsync();
        }

        public async Task<bool> StudentWithIdExistsAsync(int id)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.Student>().AnyAsync(x => x.Id == id);
        }

        public async Task<bool> UserWithIdExistsAsync(string userId)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.UserId == userId).AnyAsync();       
        }

        public async Task<bool> UserWithIdHasRentedBookAsync(int id, string userId)
        {
           return await repository.AllReadOnly<Book>().AnyAsync(x => x.Id == id && x.RenterId == userId);
        }

		public async Task<bool> SubjectWithIdExistsAsync(int id)
		{
			return await repository.AllReadOnly<Infrastructure.Data.Models.Subject>().AnyAsync(x => x.Id == id);
		}

		public async Task<bool> StudentHasSubjectAsync(int subjectId, int studentId)
		{
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).Select(x => x.UserId).FirstOrDefaultAsync();
			return await repository.AllReadOnly<SubjectForStudent>().AnyAsync(x => x.SubjectId == subjectId && x.Student.Id == student);
		}

        public async Task<EditSubjectFormViewModel?> GetEditSubjectFormAsync(int subjectId)
        {
            var subject = await repository.AllReadOnly<Infrastructure.Data.Models.Subject>().Where(x => x.Id == subjectId)
                .Select(x => new EditSubjectFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TotalAttendanceCount = x.TotlaAttendanceCount,

                })
                .FirstOrDefaultAsync();
            subject.SubjectProfessors = await GetProfessors(subjectId);
            return subject;
            
        }

        private async Task<IEnumerable<SubjectProfessorIndexViewModel>> GetProfessors(int subjectId)
        {
            return await repository.AllReadOnly<SubjectByProfessor>().Where(x => x.SubjectId == subjectId)
                .Select(x => new SubjectProfessorIndexViewModel()
                {
                    Id = x.Professor.Id,
                    FirstName = x.Professor.FirstName,
                    LastName = x.Professor.LastName,
                    Email = x.Professor.Email,
                    PhoneNumber = x.Professor.PhoneNumber
                }).ToListAsync();
        }
    }
}
