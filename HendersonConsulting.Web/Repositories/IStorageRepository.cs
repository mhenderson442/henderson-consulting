namespace HendersonConsulting.Web.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Models;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// This provides interfaces to the <see cref="IStorageRepository"/> class.
    /// </summary>
    public interface IStorageRepository
    {
        /// <summary>
        /// Get blog post list.
        /// </summary>
        /// <returns>Returns the <see cref="GetBlogPostListAsync"/> instance.</returns>
        Task<List<BlogPostYear>> GetBlogPostListAsync();

        /// <summary>
        /// Get cloud blob client
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<CloudBlobClient> GetCloudBlobClientAsync();

        /// <summary>
        /// Gets default post.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<BlogPostContent> GetDefaultPostItemAsync();

        /// <summary>
        /// get static content base url.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<string> GetStaticContentBaseUrlAsync();

        /// <summary>
        /// Gets blog post.
        /// </summary>
        /// <param name="year">year string instance.</param>
        /// <param name="month">month string instance.</param>
        /// <param name="day">day string instance.</param>
        /// <param name="name">name string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<BlogPostContent> GetBlogPostItemAsync(string year, string month, string day, string name);

        /// <summary>
        /// Gets image from blob storage.
        /// </summary>
        /// <param name="itemPath">Image path string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<CloudBlockBlob> GetImageBlobAsych(string itemPath);

        /// <summary>
        /// Gets list of categories.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        Task<List<Category>> GetCategoriesAsync();
    }
}
