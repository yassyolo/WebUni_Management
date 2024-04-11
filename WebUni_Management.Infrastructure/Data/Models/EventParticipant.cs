using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUni_Management.Infrastructure.Data.Models
{
	[Comment("Event participant entity")]
    public class EventParticipant
    {
        [Comment("Event identifier")]
        public int EventId { get; set; }

        [Comment("Event")]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        [Comment("Participant identifier")]
        public string ParticipantId { get; set; } = string.Empty;

        [Comment("Participant")]
        [ForeignKey(nameof(ParticipantId))]
        public ApplicationUser Participant { get; set; } = null!;
    }
}
