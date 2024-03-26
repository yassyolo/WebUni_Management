using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUni_Management.Core.Models.News
{
    public class NewsDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set;} = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string PublishedOn { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}
