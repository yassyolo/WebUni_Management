using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;
using WebUni_Management.Core.Models.PersonalInfo;

namespace WebUni_Management.Core.Contracts
{
    public interface IPersonalInfoService
    {
        Task<ManageAttendanceViewModel> AddAttendanceAsync(int subjectId, int studentId);
        Task AddFacultyAsync(FacultyFormViewModel model);
        Task AddMajorAsync(MajorFormViewModel model);
        Task EditFacultyAsync(int id, FacultyFormViewModel model);
        Task EditMajorAsync(int id, MajorFormViewModel model);
        Task EditSubjectAsync(int id, EditSubjectFormViewModel model);
        Task<bool> FacultyExistsByIdAsync(int id);
        Task<AllFacultiesViewModel> FilterFacultiesAsync(string searchTerm, int currentPage, int facultiesPerPage);
        Task<AllMajorsViewModel> FilterMajorsAsync(string searchTerm, int currentPage, int majorsPerPage);
        Task<SearchStudentViewModel> FilterStudentAsync(string searchTerm);
        Task<ManageAttendanceViewModel?> GetAttendanceRecordForStudentAsync(int subjectId, int studentId);
        Task<FacultyFormViewModel?> GetEditFacultyFormAsync(int id);
        Task<MajorFormViewModel?> GetEditMajorFormAsync(int id);
        Task<EditSubjectFormViewModel?> GetEditSubjectFormAsync(int subjectId, int studentId);
        Task<IEnumerable<MajorIndexViewModel>?> GetMajorsForFacultyAsync(int id);
        Task<StudentDetailsViewModel?> GetStudentDetailsByIdAsync(int id);
        Task<PersonalInfoViewModel?> LoadPersonalInfoAsync(string userId);
        Task<bool> MajorExistsByIdAsync(int id);
        Task<IEnumerable<BookInfoViewModel>> MyRentedBooksAsync(string userId);
        Task RemoveBookRentAsync(int id, string userId);
        Task<bool> RentedBookExistsByIdAsync(int id);
		Task<bool> StudentHasSubjectAsync(int subjectId, int studentId);
		Task<bool> StudentWithIdExistsAsync(int id);
		Task<bool> SubjectWithIdExistsAsync(int id);
		Task<bool> UserWithIdExistsAsync(string userId);
        Task<bool> UserWithIdHasRentedBookAsync(int id, string userId);
    }
}
