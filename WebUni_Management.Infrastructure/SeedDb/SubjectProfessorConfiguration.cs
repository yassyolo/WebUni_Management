using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class SubjectProfessorConfiguration : IEntityTypeConfiguration<SubjectProfessor>
    {
        public void Configure(EntityTypeBuilder<SubjectProfessor> builder)
        {
            var data = new SeedData();

            builder.HasData(new SubjectProfessor[] {data.SubjectProfessor1, data.SubjectProfessor2, data.SubjectProfessor3, data.SubjectProfessor4, data.SubjectProfessor5, data.SubjectProfessor6});
        }
    }
}
