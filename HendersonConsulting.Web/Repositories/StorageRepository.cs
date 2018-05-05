using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HendersonConsulting.Web.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace HendersonConsulting.Web.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly AppSettings _appSettings;

        public StorageRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private async Task<List<BlogPostItem>> GetBlogPosts(List<CloudBlockBlob> blobList)
        {
            var posts = blobList
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .ToList()
                .Select(x => new BlogPostItem
                {
                    Prefix = x.Parent.Prefix,
                    Name = FormatName(x.Name, x.Parent.Prefix)
                })
                .ToList();

            return await Task.Run(() => posts);
        }

        private async Task<List<BlogPostDay>> GetBlogDays(List<CloudBlockBlob> blobList)
        {
            var posts = await GetBlogPosts(blobList);

            var days = blobList
                .GroupBy(x => x.Parent.Prefix)
                .Select(grp => grp.First())
                .ToList()
                .Select(x => new BlogPostDay
                {
                    Prefix = x.Parent.Prefix,
                    Day = ConvertStringToInt(x.Name.Substring(8, 2)),
                    DayName = x.Name.Substring(8, 2),
                    BlogPostList = posts.Where(y => y.Prefix == x.Parent.Prefix).ToList()
                })
                .ToList();

            return await Task.Run(() => days);
        }

        private async Task<List<BlogPostMonth>> GetBlogMonths(List<CloudBlockBlob> blobList)
        {            
            var items = await GetBlogPosts(blobList);

            var months = blobList
                .GroupBy(x => x.Parent.Parent.Prefix)
                .Select(grp => grp.First())
                .ToList()
                .Select(x => new BlogPostMonth
                {
                    Prefix = x.Parent.Parent.Prefix,
                    Month = ConvertStringToInt(x.Name.Substring(5, 2)),
                    MonthName = GetMonthLong(x.Name.Substring(5, 2)),
                    BlogPostItems = items.Where(y => y.Prefix.Substring(0,8) == x.Parent.Parent.Prefix).ToList()
                })
                .ToList();

            return await Task.Run(() => months);
        }

        public async Task<BlogPostContent> GetBlogPostItemAsync(string year, string month, string day, string name)
        {
            var blobName = $"{ year }/{ month }/{ day }/{ name }.md";

            var client = await GetCloudBlobClientAsync();
            var container = client.GetContainerReference(_appSettings.BlogPostContainer);
            var blogPostItem = container.GetBlockBlobReference(blobName);

            var opContext = new OperationContext();

            var stream = await blogPostItem.OpenReadAsync(null, null, opContext);
            var datePosted = await FormatDatePostedString(blogPostItem.Parent.Prefix);

            using (var reader = new StreamReader(stream))
            {
                var contentBuilder = new StringBuilder();
                var blobContent = await reader.ReadToEndAsync();

                var blogPostContent = new BlogPostContent
                {
                    DatePosted = datePosted,
                    PageContent = blobContent
                };

                return blogPostContent;
            }
        }

        public async Task<CloudBlockBlob> GetImageBlobAsych(string itemPath)
        {
            var blobList = await GetBlobList(_appSettings.ImagesContainer);

            var imageBlob = blobList
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .FirstOrDefault(x => x.Name == itemPath);

            return await Task.Run(() => imageBlob);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var fileName = "categories.json";
            var client = await GetCloudBlobClientAsync();
            var container = client.GetContainerReference(_appSettings.StaticContainer);
            var categoriesFile = container.GetBlockBlobReference(fileName);

            var opContext = new OperationContext();
            var stream = await categoriesFile.OpenReadAsync(null, null, opContext);

            using (var reader = new StreamReader(stream))
            {
                var categories = await reader.ReadToEndAsync();
                var list = JsonConvert.DeserializeObject<List<Category>>(categories);
                list.ForEach(f => f.BlogPostItems.ForEach(fx => fx.Name = FormatName(fx.Name)));

                return list;
            }                      
        }

        public async Task<string> GetStaticContentBaseUrlAsync()
        {
            var cloudStorageAccount = GetCloudStorageAccount(_appSettings.StorageAccountName, _appSettings.StorageAccountKey);
            var staticContentUrl = await Task.Run(() => string.Format("{0}/{1}", cloudStorageAccount.BlobEndpoint.ToString().TrimEnd('/'), _appSettings.BlogPostContainer.TrimStart('/')));

            return staticContentUrl;
        }

        public async Task<BlogPostContent> GetDefaultPostItemAsync()
        {
            var blobList = await GetBlobList(_appSettings.BlogPostContainer);

            var baseDate = DateTime.Now.AddYears(-2);

            var blob = blobList
                        .Where(x => (Convert.ToInt32(x.Parent.Prefix.Substring(0, 4))) >= baseDate.Year)
                        .OrderByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(0, 4)))
                        .ThenByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(5, 2)))
                        .ThenByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(8, 2)))
                        .ThenByDescending(x => x.Properties.LastModified)
                        .FirstOrDefault();

            var opContext = new OperationContext();

            var stream = await blob.OpenReadAsync(null, null, opContext);

            var datePosted = await FormatDatePostedString(blob.Parent.Prefix);

            using (var reader = new StreamReader(stream))
            {
                var contentBuilder = new StringBuilder();

                var blobContent = await reader.ReadToEndAsync();  

                var blogPostContent = new BlogPostContent
                {
                    DatePosted = datePosted,
                    PageContent = blobContent
                };

                return blogPostContent;
            }
        }

        private async static Task<string> FormatDatePostedString(string prefix)
        {
            var year = prefix.Substring(0, 4);
            var month = GetMonthLong(prefix.Substring(5, 2));
            var day = prefix.Substring(8, 2);

            var datePostedString = await Task.Run(() =>  $"Posted { day } { month } { year }");

            return datePostedString;
        }

        private async Task<List<BlogPostYear>> GetBlogYears(List<CloudBlockBlob> blobList)
        {
           
            var months = await GetBlogMonths(blobList);

            var years = blobList
                .GroupBy(x => x.Parent.Parent.Parent.Prefix)
                .Select(grp => grp.First())
                .ToList()
                .Select(x => new BlogPostYear
                {
                    Prefix = x.Parent.Parent.Parent.Prefix,
                    Year = ConvertStringToInt(x.Name.Substring(0, 4)),
                    Months = months.Where(y => y.Prefix.Substring(0, 5) == x.Parent.Parent.Parent.Prefix).ToList()
                })
                .OrderByDescending(x => x.Year)
                .ToList();

            return await Task.Run(() => years);
        }

        private async Task<List<CloudBlockBlob>> GetBlobList(string container)
        {
            var blobClient = await GetCloudBlobClientAsync();
            var containerReference = blobClient.GetContainerReference(container);

            var results = new List<IListBlobItem>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var response = await containerReference.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);

            } while (continuationToken != null);

            var blobList = results
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .ToList();

            return blobList;
        }

        public async Task<List<BlogPostYear>> GetBlogPostListAsync()
        {
            var blobList = await GetBlobList(_appSettings.BlogPostContainer);

            if(blobList.Count == 0)
            {
                return null;
            }

            var list = await GetBlogYears(blobList);
           
            return list;
        }

        public async Task<CloudBlobClient> GetCloudBlobClientAsync()
        {
            var cloudStorageAccount = GetCloudStorageAccount(_appSettings.StorageAccountName, _appSettings.StorageAccountKey);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return await Task.Run(() => cloudBlobClient);
        }

        private static CloudStorageAccount GetCloudStorageAccount(string storageAccountName, string storageAccountKey)
        {
            var storageCredentials = new StorageCredentials(storageAccountName, storageAccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);

            return cloudStorageAccount;
        }

        private static string FormatName(string input)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(input);
        }

        private static string FormatName(string input, string prefix)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            var output = input
                .Replace(prefix, "")
                .Replace(".md", "");

            return textInfo.ToTitleCase(output);
        }

        private static int ConvertStringToInt(string input)
        {
            var result = Int32.TryParse(input, out int output);

            var converted = result ? output : 0;

            return converted;
        }

        private static string GetMonthLong(string month)
        {
            var monthLong = month;

            switch (month)
            {
                case "01":
                    monthLong = "January";
                    break;
                case "02":
                    monthLong = "February";
                    break;
                case "03":
                    monthLong = "March";
                    break;
                case "04":
                    monthLong = "April";
                    break;
                case "05":
                    monthLong = "May";
                    break;
                case "06":
                    monthLong = "June";
                    break;
                case "07":
                    monthLong = "July";
                    break;
                case "08":
                    monthLong = "August";
                    break;
                case "09":
                    monthLong = "September";
                    break;
                case "10":
                    monthLong = "October";
                    break;
                case "11":
                    monthLong = "November";
                    break;
                case "12":
                    monthLong = "December";
                    break;
                default:
                    monthLong = month;
                    break;
            }

            return monthLong;
        }
    }
}
