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
using WebUni_Management.Infrastructure.SeedDb;
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

        public async Task<EditSubjectFormViewModel?> GetEditSubjectFormAsync(int subjectId, int studentId)
        {
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).Select(x => x.UserId).FirstOrDefaultAsync();
            var subject = await repository.AllReadOnly<SubjectForStudent>().Where(x => x.SubjectId == subjectId && x.StudentId == student)
                .Select(x => new EditSubjectFormViewModel()
                {
                    SubjectId = x.Subject.Id,
                    Name = x.Subject.Name,
                    Description = x.Subject.Description,
                    TotalAttendanceCount = x.Subject.TotlaAttendanceCount,
                    StudentId = subjectId,

                })
                .FirstOrDefaultAsync();
           var subjectProfessor = await repository.AllReadOnly<SubjectByProfessor>().Where(x => x.SubjectId == subjectId && x.Professor.Title == "Professor")
                .Select(x => new SubjectProfessorIndexViewModel()
                {
                    Id = x.Professor.Id,
                    FirstName = x.Professor.FirstName,
                    LastName = x.Professor.LastName,
                    Email = x.Professor.Email,
                    Title = x.Professor.Title,
                    Description = x.Professor.Description,
                    PhoneNumber = x.Professor.PhoneNumber,
                }).FirstOrDefaultAsync();
            if(subjectProfessor != null)
            {
                subject.SubjectProfessor = subjectProfessor;
            }
            var subjectAssistant = await repository.AllReadOnly<SubjectByProfessor>().Where(x => x.SubjectId == subjectId && x.Professor.Title == "Assistant")
                .Select(x => new SubjectAssistantIndexViewModel()
                {
                    Id = x.Professor.Id,
                    FirstName = x.Professor.FirstName,
                    LastName = x.Professor.LastName,
                    Email = x.Professor.Email,
                    Title = x.Professor.Title,
                    Description = x.Professor.Description,
                    PhoneNumber = x.Professor.PhoneNumber,
                }).FirstOrDefaultAsync();
            if (subjectAssistant != null)
            {
                subject.SubjectAssistant = subjectAssistant;
            }
            return subject;
            
        }

        public async Task EditSubjectAsync(int id, EditSubjectFormViewModel model)
        {
            var subject = await repository.All<Infrastructure.Data.Models.Subject>().FirstOrDefaultAsync(x => x.Id == id);
            subject.Name = model.Name;
            subject.Description = model.Description;
            subject.TotlaAttendanceCount = model.TotalAttendanceCount;
            var professors = await repository.All<SubjectByProfessor>().Where(x => x.SubjectId == id).ToListAsync();
            foreach (var p in professors)
            {
                var professor = await repository.All<SubjectProfessor>().FirstOrDefaultAsync(x => x.Id == p.ProfessorId);
                if(professor.Title == "Professor")
                {
                    
                    professor.Title = model.SubjectProfessor.Title;
                    professor.PhoneNumber = model.SubjectProfessor.PhoneNumber;
                    professor.Email = model.SubjectProfessor.Email;
                    professor.FirstName = model.SubjectProfessor.FirstName;
                    professor.LastName = model.SubjectProfessor.LastName;
                    professor.Description = model.SubjectProfessor.Description;
                }
                else if(professor.Title == "Assistant")
                {
                    
                    professor.Title = model.SubjectProfessor.Title;
                    professor.PhoneNumber = model.SubjectProfessor.PhoneNumber;
                    professor.Email = model.SubjectProfessor.Email;
                    professor.FirstName = model.SubjectProfessor.FirstName;
                    professor.LastName = model.SubjectProfessor.LastName;
                    professor.Description = model.SubjectProfessor.Description;
                }
            }
            await repository.SaveChangesAsync();
        }

        public async Task<AllFacultiesViewModel> FilterFacultiesAsync(string? searchTerm = null, int currentPage = 1, int facultiesPerPage = 4)
        {
            var faculties = repository.AllReadOnly<Infrastructure.Data.Models.Faculty>();

            if (searchTerm != null && !string.IsNullOrWhiteSpace(searchTerm))
            {
                var normalizedSearchTerm = searchTerm.ToLower();
                faculties = faculties.Where(x => x.Name.Contains(normalizedSearchTerm));
            }
            var facultiesToShow = await faculties.Skip((currentPage - 1) * facultiesPerPage).Take(facultiesPerPage)
                .Select(x => new FacultyIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
               
                }).ToListAsync();
            return new AllFacultiesViewModel()
            {
                TotalFaculties = await faculties.CountAsync(),
                Faculties = facultiesToShow
            };
        }

        public async Task AddFacultyAsync(FacultyFormViewModel model)
        {
            var faculty = new Faculty()
            {
                Name = model.Name,
            };
            
            await repository.AddAsync(faculty);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> FacultyExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Faculty>().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MajorIndexViewModel>?> GetMajorsForFacultyAsync(int id)
        {
            return await repository.AllReadOnly<Major>().Where(x => x.FacultyId == id)
                .Select(x => new MajorIndexViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        }

        public async Task<FacultyFormViewModel?> GetEditFacultyFormAsync(int id)
        {
            return await repository.AllReadOnly<Faculty>().Where(x => x.Id == id)
                .Select(x => new FacultyFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).FirstOrDefaultAsync();
        }

        public Task EditFacultyAsync(int id, FacultyFormViewModel model)
        {
            var faculty = repository.All<Faculty>().FirstOrDefault(x => x.Id == id);
            faculty.Name = model.Name;
            return repository.SaveChangesAsync();
        }

        public async Task<AllMajorsViewModel> FilterMajorsAsync(string? searchTerm = null, int currentPage = 1, int majorsPerPage = 4)
        {
            var majors = repository.AllReadOnly<Major>();
            if (searchTerm != null)
            {
                var normalizedSearchTerm = searchTerm.ToLower();
                majors = majors.Where(x => x.Name.Contains(normalizedSearchTerm));
            }
            var majorsToShow = await majors.Skip((currentPage - 1) * majorsPerPage).Take(majorsPerPage)
                .Select(x => new MajorIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
            return new AllMajorsViewModel()
            {
                Majors = majorsToShow,
                TotalMajors = await majors.CountAsync(),
            };


        }

        public async Task<bool> MajorExistsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Major>().AnyAsync(x => x.Id == id);
        }

        public Task<MajorFormViewModel?> GetEditMajorFormAsync(int id)
        {
            return repository.AllReadOnly<Major>().Where(x => x.Id == id)
                .Select(x => new MajorFormViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).FirstOrDefaultAsync();
        }

        public async Task EditMajorAsync(int id, MajorFormViewModel model)
        {
            var major = await repository.All<Major>().FirstOrDefaultAsync(x => x.Id == id);
            major.Name = model.Name;
            await repository.SaveChangesAsync();
        }

        public async Task AddMajorAsync(MajorFormViewModel model)
        {
            try
            {
                var facultyId = await repository.AllReadOnly<Faculty>().Where(x => x.Name == model.FacultyName).Select(x => x.Id).FirstOrDefaultAsync();

                var major = new Major()
                {
                    Name = model.Name,
                    FacultyId = facultyId
                };
                await repository.AddAsync(major);
                await repository.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidCastException("Faculty does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured", ex);
            }
                       
        }

        public async Task<ManageAttendanceViewModel?> GetAttendanceRecordForStudentAsync(int subjectId, int studentId)
        {
            var studentUserId = repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).Select(x => x.UserId).FirstOrDefault();
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).FirstOrDefaultAsync();
            var subject = await repository.AllReadOnly<Infrastructure.Data.Models.Subject>().Where(x => x.Id == subjectId).FirstOrDefaultAsync();

            return await repository.AllReadOnly<SubjectForStudent>().Where(x => x.SubjectId == subjectId && x.StudentId == studentUserId)
                .Select(x => new ManageAttendanceViewModel()
                {
                    SubjectId = subjectId,
                    StudentId = studentId,
                    StudentFirstName = student.FirstName,
                    StudentLastName = student.LastName,
                    FacultyNumber = student.FacultyNumber,
                    SubjectName = subject.Name,
                    SubjectTotalAttendance = subject.TotlaAttendanceCount,
                    StudentAttendanceRecord = x.AttendanceRecord,
                    RemainingAttendance = subject.TotlaAttendanceCount - x.AttendanceRecord,
                }).FirstOrDefaultAsync();
        }

        public async Task<ManageAttendanceViewModel> AddAttendanceAsync(int subjectId, int studentId)
        {
            var studentUserId = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).Select(x => x.UserId).FirstOrDefaultAsync();
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.Id == studentId).FirstOrDefaultAsync();
            var subject = await repository.AllReadOnly<Infrastructure.Data.Models.Subject>().Where(x => x.Id == subjectId).FirstOrDefaultAsync();
            var subjectForStudent = await repository.All<SubjectForStudent>().Where(x => x.SubjectId == subjectId && x.StudentId == studentUserId).FirstOrDefaultAsync();
            subjectForStudent.AttendanceRecord += 1;
            await repository.SaveChangesAsync();
            return await repository.AllReadOnly<SubjectForStudent>().Where(x => x.SubjectId == subjectId && x.StudentId == studentUserId)
               .Select(x => new ManageAttendanceViewModel()
               {
                   SubjectId = subjectId,
                   StudentId = studentId,
                   StudentFirstName = student.FirstName,
                   StudentLastName = student.LastName,
                   FacultyNumber = student.FacultyNumber,
                   SubjectName = subject.Name,
                   SubjectTotalAttendance = subject.TotlaAttendanceCount,
                   StudentAttendanceRecord = x.AttendanceRecord + 1,
                   RemainingAttendance = subject.TotlaAttendanceCount - (x.AttendanceRecord + 1),
               }).FirstOrDefaultAsync();
        }

        public async Task<PersonalInfoViewModel?> LoadPersonalInfoAsync(string userId)
        {
            var student = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.UserId == userId).FirstOrDefaultAsync();
            var faculty = await repository.AllReadOnly<Faculty>().Where(x => x.Id == student.FacultyId).Select(x => x.Name).FirstOrDefaultAsync();
            if(faculty == null)
            {
                return null;
            }
            var major = await repository.AllReadOnly<Major>().Where(x => x.Id == student.MajorId).Select(x => x.Name).FirstOrDefaultAsync();
           var courseTerm = await repository.AllReadOnly<CourseTerm>().Where(x => x.Id == student.CourseTermId).Select(x => x.Name).FirstOrDefaultAsync();
        
            var studentPersonalInfo = await repository.AllReadOnly<Infrastructure.Data.Models.Student>().Where(x => x.UserId == userId)
               .Select(x => new PersonalInfoViewModel()
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   Faculty = faculty,
                   Major = major,
                   FacultyNumber = x.FacultyNumber,
                   CourseTerm= courseTerm,

               }).FirstOrDefaultAsync();
            studentPersonalInfo.Subjects = await GetSubjects(userId);
            return studentPersonalInfo;
        }
    }
}
