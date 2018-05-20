namespace HendersonConsulting.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Category model.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets <see cref="Name" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets <see cref="BlogPostItems" />.
        /// </summary>
        public List<BlogPostItem> BlogPostItems { get; set; }
    }
}
