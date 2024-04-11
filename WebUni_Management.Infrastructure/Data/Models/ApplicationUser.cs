using Microsoft.AspNetCore.Identity;

namespace WebUni_Management.Infrastructure.Data.Models
{
	public class ApplicationUser : IdentityUser
    {
        public bool IsApproved { get; set; }

        public string InitialPassword { get; set; } = string.Empty;
    }
}
