using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
    public class SubjectByProfessorConfiguration : IEntityTypeConfiguration<SubjectByProfessor>
    {
        public void Configure(EntityTypeBuilder<SubjectByProfessor> builder)
        {
            builder.HasKey(e => new { e.SubjectId, e.ProfessorId });
            var data = new SeedData();  
            builder.HasData(new SubjectByProfessor[] {data.Subject1ByProfessor1, data.Subject1ByProfessor2, data.Subject2ByProfessor3, data.Subject3ByProfessor4});
        }
    }
}
