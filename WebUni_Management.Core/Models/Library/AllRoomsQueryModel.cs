using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Library
{
    public class AllRoomsQueryModel
    {
        public int CurrentPage { get; set; } = 1;
        public int RoomsPerPage { get; set; } = 3;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Capacity { get; set; } 
        public string? SearchTerm { get; set; } = string.Empty;
        public int TotalRooms { get; set; }
        public IEnumerable<RoomShowcaseViewModel> StudyRooms { get; set; } = new List<RoomShowcaseViewModel>();
    }
}
