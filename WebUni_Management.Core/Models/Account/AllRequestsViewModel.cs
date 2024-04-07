using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Account
{
    public class AllRequestsViewModel
    {
        public int RequestsPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalRequests { get; set; }

        public IEnumerable<RequestsViewModel> Requests = new List<RequestsViewModel>();
    }
}
