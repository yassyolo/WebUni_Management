using Microsoft.EntityFrameworkCore;
using WebUni_Management.Core.Contracts;
using WebUni_Management.Core.Models.Account;
using WebUni_Management.Infrastructure.Data.Models;
using QRCoder;
using WebUni_Management.Infrastructure.Repository;

namespace WebUni_Management.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repository;

        public AccountService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddNewStudentAsync(string username, ManageAccountViewModel model)
        {
            var majorId = await repository.AllReadOnly<Major>().Where(x => x.Name == model.Major).Select(x => x.Id).FirstOrDefaultAsync();
            var facultyId = await repository.AllReadOnly<Faculty>().Where(x => x.Name == model.Faculty).Select(x => x.Id).FirstOrDefaultAsync();
            var courseTermId = await repository.AllReadOnly<CourseTerm>().Where(x => x.Name == model.CourseTerm).Select(x => x.Id).FirstOrDefaultAsync();
            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                MajorId = majorId,
                FacultyNumber = username,
                FacultyId = facultyId,
                CourseTermId = courseTermId,
            };
            await repository.AddAsync(student);
            await  repository.SaveChangesAsync();
        }

        public async Task<ManageAccountViewModel> FillManageAccountAsync(string userId)
		{
            var student = await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x => x.UserId == userId);
            if (student == null)
            {
                throw new NotFoundException(nameof(Student));
            }
			var faculty = await repository.AllReadOnly<Faculty>().FirstOrDefaultAsync(x => x.Id == student.FacultyId);
			if (faculty == null)
			{
				throw new NotFoundException(nameof(Faculty));
			}
			var major = await repository.AllReadOnly<Major>().FirstOrDefaultAsync(x => x.Id == student.MajorId);
			if (major == null)
			{
				throw new NotFoundException(nameof(Major));
			}
			var courseTerm = await repository.AllReadOnly<CourseTerm>().FirstOrDefaultAsync(x => x.Id == student.CourseTermId);
			if (courseTerm == null)
			{
                throw new NotFoundException(nameof(CourseTerm));
            }

            return await repository.AllReadOnly<Student>().Where(x => x.UserId == userId).Select(x => new ManageAccountViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                PhoneNumber = x.PhoneNumber,
                FacultyNumber = x.FacultyNumber,
                Faculty = faculty.Name,
                Major = major.Name,
                Email = x.User.Email,
                CourseTerm = courseTerm.Name,
            }).FirstOrDefaultAsync();          
		}

        public async Task<string> GetQrCodeForStudentAsync(string userId)
        {
            var student = await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x => x.UserId == userId);

            if (student == null)
            {
                throw new InvalidOperationException("Student not found");
            }

            var qrCode = student.QRCode;
            string qrCodeString = qrCode != null ? Convert.ToBase64String(qrCode) : null;
            return qrCodeString;
        }

		public async Task<ApplicationUser?> FindUserByIdAsync(string user)
        {
            return await repository.AllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == user);
        }

        public async Task<AllRequestsViewModel> GetAllRequestsAsync(int currentPage = 1, int requestsPerPage = 10)
        {
            var requests = repository.AllReadOnly<ApplicationUser>()
                .Where(x => x.IsApproved == false);

            var requestsToShow = await requests
                .Skip((currentPage - 1) * requestsPerPage)
                .Take(requestsPerPage)
                .Select(x => new RequestsViewModel
                {
                    UserName = x.UserName,
                    InitialPassword = x.InitialPassword,
                    Email = x.Email
                }).ToListAsync();
            return new AllRequestsViewModel
            {
                TotalRequests = await requests.CountAsync(),
                Requests = requestsToShow
            };
        }

        public async Task<bool> GetStudentAsync(string userId)
        {
            var user = await repository.AllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var student = await repository.AllReadOnly<Student>().FirstOrDefaultAsync(x => x.UserId == userId);
                if (student != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<ApplicationUser?> GetUserByUserNameAsync(string username)
        {
            return await repository.AllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task AddStudentAsync(string userId, ManageAccountViewModel model)
        {
            var student = new Student
            {
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                FacultyNumber = model.FacultyNumber,
                MajorId = await repository.AllReadOnly<Major>().Where(x => x.Name == model.Major).Select(x => x.Id).FirstOrDefaultAsync(),
                FacultyId = await repository.AllReadOnly<Faculty>().Where(x => x.Name == model.Faculty).Select(x => x.Id).FirstOrDefaultAsync(),
                CourseTermId = await repository.AllReadOnly<CourseTerm>().Where(x => x.Name == model.CourseTerm).Select(x => x.Id).FirstOrDefaultAsync()
            };
            student.QRCode = GenerateQRCode(student.FacultyNumber);
            await repository.AddAsync(student);
            await repository.SaveChangesAsync();
        }
        public byte[] GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            return qrCodeAsPngByteArr;
        }

        public async Task<bool> StudentExistsByIdAsync(int id)
		{
			return await repository.AllReadOnly<Student>().AnyAsync(x => x.Id == id);
		}

		public async Task<ManageAccountViewModel?> GetEditAccountFormAsync(int id)
		{
			return await repository.AllReadOnly<Student>().Where(x => x.Id == id).Select(x => new ManageAccountViewModel
            {
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Age = x.Age,
				PhoneNumber = x.PhoneNumber,
				FacultyNumber = x.FacultyNumber,
				Faculty = x.Faculty.Name,
				Major = x.Major.Name,
				CourseTerm = x.CourseTerm.Name
			}).FirstOrDefaultAsync();
		}

        public async Task EditAccountAsync(int id, ManageAccountViewModel model)
        {
            var student = repository.All<Student>().FirstOrDefault(x => x.Id == id);
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Age = model.Age;
            student.PhoneNumber = model.PhoneNumber;
            student.FacultyNumber = model.FacultyNumber;
            student.MajorId = repository.AllReadOnly<Major>().Where(x => x.Name == model.Major).Select(x => x.Id).FirstOrDefault();
            student.FacultyId = repository.AllReadOnly<Faculty>().Where(x => x.Name == model.Faculty).Select(x => x.Id).FirstOrDefault();
            student.CourseTermId = repository.AllReadOnly<CourseTerm>().Where(x => x.Name == model.CourseTerm).Select(x => x.Id).FirstOrDefault();
            await repository.SaveChangesAsync();
        }
    }
}
