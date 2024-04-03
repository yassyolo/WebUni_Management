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
        Task<SearchStudentViewModel> FilterStudentAsync(string searchTerm);
        Task<EditSubjectFormViewModel?> GetEditSubjectFormAsync(int subjectId);
        Task<StudentDetailsViewModel?> GetStudentDetailsByIdAsync(int id);
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
