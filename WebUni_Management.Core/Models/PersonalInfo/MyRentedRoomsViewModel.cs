using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;

namespace WebUni_Management.Core.Models.PersonalInfo
{
    public class MyRentedRoomsViewModel
    {
        public int RoomsPerPage { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public int TotalRooms { get; set; }
        public IEnumerable<RoomShowcaseViewModel> Rooms { get; set; } = new List<RoomShowcaseViewModel>();
    }
}
