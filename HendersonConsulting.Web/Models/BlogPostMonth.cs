namespace HendersonConsulting.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Blog post month post content model.
    /// </summary>
    public class BlogPostMonth
    {
        /// <summary>
        /// Gets or sets <see cref="Prefix" />.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Month" />.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets <see cref="MonthName" />.
        /// </summary>
        public string MonthName { get; set; }

        /// <summary>
        /// Gets or sets <see cref="BlogPostItems" />.
        /// </summary>
        public List<BlogPostItem> BlogPostItems { get; set; }
    }
}
