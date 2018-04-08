using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class BlogPostMonth
    {
        public string Prefix { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; }

        public List<BlogPostItem> BlogPostItems { get; set; }
    }
}
