namespace HendersonConsulting.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Blog post day model.
    /// </summary>
    public class BlogPostDay
    {
        /// <summary>
        /// Gets or sets <see cref="Prefix" />.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Day" />.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DayName" />.
        /// </summary>
        public string DayName { get; set; }

        /// <summary>
        /// Gets or sets <see cref="BlogPostList" />.
        /// </summary>
        public List<BlogPostItem> BlogPostList { get; set; }
    }
}
