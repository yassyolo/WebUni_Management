using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Event;
using WebUni_Management.Core.Models.News;

namespace WebUni_Management.Core.Models.Home
{
    public class IndexPageViewModel
    {
        public IEnumerable<NewsArticleIndexViewModel> News { get; set; } = new List<NewsArticleIndexViewModel>();
        public IEnumerable<EventIndexViewModel> Events { get; set; } = new List<EventIndexViewModel>();
    }
}
