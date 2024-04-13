using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;
using WebUni_Management.Core.Models.PersonalInfo;
using WebUni_Management.Infrastructure.Data.Models;

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
        Task<FacultyDetailsViewModel?> GetFacultyDetailsAsync(int id);
        Task<MajorDetailsViewModel?> GetMajorDetailsAsync(int id);
        Task<IEnumerable<MajorIndexViewModel>?> GetMajorsForFacultyAsync(int id);
        Task<StudentDetailsViewModel?> GetStudentDetailsByIdAsync(int id);
        Task<int> GetStudentIdByUserIdAsync(string userId);
        Task<MyJoinedEventsViewModel> JoinedEventsAsync(string userId, int currentPage, int eventsPerPage);
        Task<PersonalInfoViewModel?> LoadPersonalInfoAsync(string userId);
        Task<bool> MajorExistsByIdAsync(int id);
        Task<MyRentedBooksViewModel> MyRentedBooksAsync(string userId, int currentPage, int booksPerPage);
        Task<MyRentedRoomsViewModel> MyRentedRoomsAsync(string stringId, int currentPage, int roomsPerPage);
        Task RemoveBookRentAsync(int id, string userId);
        Task RemoveJoinAsync(int id, string userId);
        Task<bool> RentedBookExistsByIdAsync(int id);
        Task<MyAttendanceViewModel?> SeeMyAttendanceRecordAsync(int id, string studentUserId);
		Task<SeeMySubjectDetailsViewModel?> SeeMySubjectDetailsAsync(int id, string userId);
		Task<bool> StudentHasSubjectAsync(int subjectId, int studentId);
		Task<bool> StudentWithIdExistsAsync(int id);
		Task<bool> SubjectWithIdExistsAsync(int id);
        Task<bool> UserHasJoinedEventWithIdAsync(string userId);
        Task<bool> UserWithIdExistsAsync(string userId);
        Task<bool> UserWithIdHasRentedBookAsync(int id, string userId);
        Task<IEnumerable<SubjectIndexViewModel>> GetSubjects(string userId);
        Task DeleteFacultyAsync(int id);
        Task<ProfessorDetailsViewModel> GetProfessorForSubjectAsync(int id);
        Task<AssistantDetailsViewModel> GetAssistantForSubjectAsync(int id);
        Task<IEnumerable<MajorDetailsViewModel>> GetMajorsForFacultyByIdAsync(int id);
        Task<IEnumerable<SubjectIndexViewModel>> GetSubjectsForMajorAsync(int id);
        Task<EditSubjectFormViewModel?> GetEditSubjectFormForMajorAsync(int id);
        Task<int> GetMajorIdBySubjectIdAsync(int id);
		Task<SeeMySubjectDetailsViewModel?> GetSubjectDetailsById(int id);
        Task<SubjectGradeViewModel?> GetGradeForStudentAsync(int subjectId, int studentId);
		Task ManageGradeForStudentAsync(int subjectId, int studentId, SubjectGradeViewModel model);
		Task<Subject> AddSubjectForStudentAsync(int id, EditSubjectFormViewModel model);
		Task<SubjectProfessor> AddSubjectProfessorForStudentAsync(int subjectId, EditSubjectFormViewModel model);
		Task<SubjectProfessor> AddSubjectAssistantForStudentAsync(int subjectId, EditSubjectFormViewModel model);
		Task AddFullSubjectForStudentAsync(int subjectId, int studentId, int professorId, int assistantId);
	}
}
