using System.Collections.Generic;

namespace HendersonConsulting.Web.Models
{
    public class Category
    {
        public string Name { get; set; }

        public List<BlogPostItem> BlogPostItems { get; set; }

    }
}
