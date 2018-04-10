using HendersonConsulting.Web.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Repositories
{
    public interface IStorageRepository
    {
        Task<List<BlogPostYear>> GetBlogPostListAsync();
        Task<CloudBlobClient> GetCloudBlobClientAsync();
        Task<BlogPostContent> GetDefaultPostItemAsync();
        Task<string> GetStaticContentBaseUrlAsync();
        Task<BlogPostContent> GetBlogPostItemAsync(string year, string month, string day, string name);
        Task<CloudBlockBlob> GetImageBlobAsych(string itemPath);
    }
}
