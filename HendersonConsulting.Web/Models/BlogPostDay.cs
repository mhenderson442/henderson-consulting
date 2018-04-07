using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class BlogPostDay
    {
        public string Prefix { get; set; }
        
        public string Day { get; set; }

        public List<BlogPostItem> BlogPostList { get; set; }
    }
}
