namespace HendersonConsulting.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Blog post year post content model.
    /// </summary>
    public class BlogPostYear
    {
        /// <summary>
        /// Gets or sets <see cref="Prefix" />.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Year" />.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Months" />.
        /// </summary>
        public List<BlogPostMonth> Months { get; set; }       
    }
}
