using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.PersonalInfo
{
	public class AssistantDetailsViewModel
	{
		public int Id { get; set; }

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}
