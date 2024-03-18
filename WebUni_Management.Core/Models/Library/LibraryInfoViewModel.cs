using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Library
{
    public class LibraryInfoViewModel
    {
        public IEnumerable<BookInfoViewModel> BookInfoViewModel { get; set; } = null!;
        public IEnumerable<StudyRoomInfo >StudyRoomInfo { get; set; } = null!;
    }
}
