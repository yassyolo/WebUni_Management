using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Infrastructure.Data.Models
{
    [Comment("Admin")]
    public class Admin
    {
        [Comment("Admin identifier")]
        public int Id { get; set; }

        [Comment("Admin")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("Admin identifier")]
        public string UserId { get; set; } = string.Empty;
    }
}
