namespace HendersonConsulting.Web.Models
{
    /// <summary>
    /// Secrets stored in Azure data vault.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets <see cref="StorageConnectionString" />.
        /// </summary>
        public string StorageConnectionString { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StorageAccountName" />.
        /// </summary>
        public string StorageAccountName { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StorageAccountKey" />.
        /// </summary>
        public string StorageAccountKey { get; set; }

        /// <summary>
        /// Gets or sets <see cref="BlogPostContainer" />.
        /// </summary>
        public string BlogPostContainer { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ImagesContainer" />.
        /// </summary>
        public string ImagesContainer { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StaticContainer" />.
        /// </summary>
        public string StaticContainer { get; set; }
    }
}
