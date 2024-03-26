using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebUni_Management.Infrastructure.Data.Constants.ModelConstants.Event;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Event entity")]
    public class Event
    {
        [Key]
        [Comment("Event identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Guest participant for the event")]
        [MaxLength(GuestParticipantMaxLength)]
        public string GuestParticipant { get; set; } = string.Empty;

        [Required]
        [Comment("Event name")]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Event description")]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Event start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        [Comment("Event end time")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required]
        [Comment("Event image URL")]
        [MaxLength(EventImageUrlMaxLength)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Event capacity")]
        [Range(EventCapacityMinValue, EventCapacityMaxValue)]
        public int Capacity { get; set; }
    }
}
