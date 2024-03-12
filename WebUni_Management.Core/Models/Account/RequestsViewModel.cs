using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.Account
{
    public class RequestsViewModel
    {
        public string UserName { get; set; } = string.Empty;

        public string InitialPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
