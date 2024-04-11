using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class SubjectByProfessorConfiguration : IEntityTypeConfiguration<SubjectByProfessor>
    {
        public void Configure(EntityTypeBuilder<SubjectByProfessor> builder)
        {
            builder.HasKey(e => new { e.SubjectId, e.ProfessorId });

            var data = new SeedData();  

            builder.HasData(new SubjectByProfessor[] {data.Subject1ByProfessor1, data.Subject1ByProfessor2, data.Subject2ByProfessor3, data.Subject3ByProfessor4, data.Subject2ByProfessor5, data.Subject3ByProfessor6});
        }
    }
}
