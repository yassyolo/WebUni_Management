using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.News
{
	public class ApproveNewsViewModel
	{
		public int NewsPerPage { get; set; } = 2;
		public int CurrentPage { get; set; } = 1;
		public int TotalNews { get; set; }
		public IEnumerable<NewsDetailsViewModel> News { get; set; } = null!;
	}
}
