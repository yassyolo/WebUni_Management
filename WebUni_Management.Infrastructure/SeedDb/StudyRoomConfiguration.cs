using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class StudyRoomConfiguration : IEntityTypeConfiguration<StudyRoom>
    {
        public void Configure(EntityTypeBuilder<StudyRoom> builder)
        {
            builder
            .HasOne(b => b.Renter) 
            .WithMany()    
            .HasForeignKey(b => b.RenterId)
            .IsRequired(false);

            builder
            .HasOne(x => x.Library)
            .WithMany(x => x.StudyRooms)
            .HasForeignKey(x => x.LibraryId);

            var data = new SeedData();

            builder.HasData(new StudyRoom[] { data.SmallStudyRoom, data.MediumStudyRoom, data.BigStudyRoom, data.SingleStudyRoom });
        }
    }
}
