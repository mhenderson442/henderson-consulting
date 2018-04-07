using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class BlogPostYear
    {
        public string Prefix { get; set; }

        public string Year { get; set; }

        public List<BlogPostMonth> Months { get; set; } 

    }
}
