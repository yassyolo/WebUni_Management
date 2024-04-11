using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.HasKey(x => new { x.EventId, x.ParticipantId }); 

            builder.HasOne(x => x.Event)
                .WithMany()
                .HasForeignKey(x => x.EventId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            var data = new SeedData();

            builder.HasData(new EventParticipant[] { data.EventParticipant1 });
        }
    }
}
