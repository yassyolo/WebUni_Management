using Microsoft.EntityFrameworkCore;

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
