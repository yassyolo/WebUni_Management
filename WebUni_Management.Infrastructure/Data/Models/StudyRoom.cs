using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.StudyRoom;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Study room entity")]
    public class StudyRoom
    {
        [Required]
        [Comment("Study room identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(RoomNameMaxLength)]
        [Comment("Study room name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(RoomDescriptionMaxLength)]
        [Comment("Study room description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(RoomImageUrlMaxLength)]
        [Comment("Study room image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(RoomFloorMinValue, RoomFloorMaxValue)]
        [Comment("Study room floor location")]
        public int Floor { get; set; }

        [Required]
        [Range(RoomCapacityMinValue, int.MaxValue)]
        [Comment("Study room capacity")]
        public int Capacity { get; set; }

        [Comment("Is study room rented")]
        public bool IsRented { get; set; }

        [Comment("Study room renter")]
        [ForeignKey(nameof(RenterId))]
        public ApplicationUser? Renter { get; set; }

        [Comment("Study room renter identifier")]
        public string? RenterId { get; set; }

        [Comment("Rent date of the room")]
        public DateTime? RentalDate { get; set; }

        [Comment("Library identifier")]
        public int? LibraryId { get; set; }

        [Comment("Library")]
        [ForeignKey(nameof(LibraryId))]
        public Library Library { get; set; } = null!;
    }
}
