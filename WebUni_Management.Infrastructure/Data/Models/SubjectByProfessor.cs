using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Subject by professor entity")]
    public class SubjectByProfessor
    {
        [Comment("Subject identifier")]
        public int SubjectId { get; set; }

        [Comment("Subject")]
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;

        [Comment("Professor identifier")]
        public int ProfessorId { get; set; }

        [Comment("Professor")]
        [ForeignKey(nameof(ProfessorId))]
        public SubjectProfessor Professor { get; set; } = null!;
    }
}
