using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class BlogPostDay
    {
        public string Prefix { get; set; }
        
        public int Day { get; set; }

        public string DayName { get; set; }

        public List<BlogPostItem> BlogPostList { get; set; }
    }
}
