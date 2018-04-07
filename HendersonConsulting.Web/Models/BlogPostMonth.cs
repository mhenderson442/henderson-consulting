using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class BlogPostMonth
    {
        public string Prefix { get; set; }

        public string Month { get; set; }

        public List<BlogPostDay> BlogPostDayList { get; set; }
    }
}
