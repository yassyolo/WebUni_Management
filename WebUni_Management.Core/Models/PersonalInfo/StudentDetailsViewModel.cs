using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FacultyNumber { get; set; } = string.Empty;
        public int RentedBooksCount { get; set; }
        public int RentedStudyRoomsCount { get; set; }
        public IEnumerable<SubjectIndexViewModel> Subjects { get; set; } = new List<SubjectIndexViewModel>();
    }
}
