using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Core.Models.Constants.MessageConstants;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.StudyRoom;

namespace WebUni_Management.Core.Models.Library
{
    public class EditRoomViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RoomImageUrlMaxLength, MinimumLength = RoomImageUrlMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RoomNameMaxLength, MinimumLength = RoomNameMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(RoomFloorMinValue, RoomFloorMaxValue, ErrorMessage = InvalidFloor)]
        public int Floor { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(RoomCapacityMinValue, int.MaxValue, ErrorMessage = InvalidCapacity)]
        public int Capacity { get; set; }
        public bool IsRented { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(RoomDescriptionMaxLength, MinimumLength = RoomDescriptionMinLength, ErrorMessage = MaxLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

    }
}
