namespace HendersonConsulting.Web.Repositories
{
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

    /// <summary>
    /// Storage repository class.
    /// </summary>
    public class StorageRepository : IStorageRepository
    {
        /// <summary>
        /// <see cref="AppSettings" /> instance.
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageRepository" /> class.
        /// </summary>
        /// <param name="appSettings"><see cref="AppSettings" /> instance.</param>
        public StorageRepository(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

        /// <summary>
        /// Get list of blog months.
        /// </summary>
        /// <param name="year">year string instance.</param>
        /// <param name="month">month string instance.</param>
        /// <param name="day">day string instance.</param>
        /// <param name="name">name string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<BlogPostContent> GetBlogPostItemAsync(string year, string month, string day, string name)
        {
            var blobName = $"{ year }/{ month }/{ day }/{ name }.md";
            var client = await GetCloudBlobClientAsync();

            var container = client.GetContainerReference(_appSettings.BlogPostContainer);
            var blogPostItem = container.GetBlockBlobReference(blobName);

            var stream = await blogPostItem.OpenReadAsync(null, null, new OperationContext());
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

        /// <summary>
        /// Get list of type <see cref="BlogPostYear"/>.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<List<BlogPostYear>> GetBlogPostListAsync()
        {
            var blobList = await GetBlobList(_appSettings.BlogPostContainer);

            if (blobList.Count == 0)
            {
                return null;
            }

            var list = await GetBlogYears(blobList);

            return list;
        }

        /// <summary>
        /// Gets list of categories.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var client = await GetCloudBlobClientAsync();
            var container = client.GetContainerReference(containerName: _appSettings.StaticContainer);

            var categoriesFile = container.GetBlockBlobReference("categories.json");
            var operatopnContext = new OperationContext();

            var stream = await categoriesFile.OpenReadAsync(null, null, operatopnContext);

            using (var reader = new StreamReader(stream))
            {
                var list = JsonConvert.DeserializeObject<List<Category>>(await reader.ReadToEndAsync());
                list.ForEach(f => f.BlogPostItems.ForEach(fx => fx.Name = FormatName(fx.Name)));

                return list;
            }
        }

        /// <summary>
        /// Gets <see cref="CloudBlobClient"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<CloudBlobClient> GetCloudBlobClientAsync()
        {
            var cloudStorageAccount = GetCloudStorageAccount(_appSettings.StorageAccountName, _appSettings.StorageAccountKey);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return await Task.Run(function: () => cloudBlobClient);
        }

        /// <summary>
        /// Gets default post.
        /// </summary>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<BlogPostContent> GetDefaultPostItemAsync()
        {
            var blobList = await GetBlobList(_appSettings.BlogPostContainer);
            var baseDate = DateTime.Now.AddYears(-2);

            var blob = blobList
                        .Where(x => Convert.ToInt32(x.Parent.Prefix.Substring(0, 4)) >= baseDate.Year)
                        .OrderByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(0, 4)))
                        .ThenByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(5, 2)))
                        .ThenByDescending(x => Convert.ToInt32(x.Parent.Prefix.Substring(8, 2)))
                        .ThenByDescending(x => x.Properties.LastModified)
                        .FirstOrDefault();

            var operationContext = new OperationContext();
            var stream = await blob.OpenReadAsync(null, null, operationContext);

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

        /// <summary>
        /// Get image from blob storage.
        /// </summary>
        /// <param name="itemPath">Image path string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task<CloudBlockBlob> GetImageBlobAsych(string itemPath)
        {
            var blobList = await GetBlobList(_appSettings.ImagesContainer);

            var imageBlob = blobList
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .FirstOrDefault(x => x.Name == itemPath);

            return await Task.Run(() => imageBlob);
        }

        /// <summary>
        /// get static content base url.
        /// </summary>
        /// <returns>Returns the <see cref="GetBlogPostListAsync"/> instance.</returns>
        public async Task<string> GetStaticContentBaseUrlAsync()
        {
            var cloudStorageAccount = GetCloudStorageAccount(_appSettings.StorageAccountName, _appSettings.StorageAccountKey);
            var staticContentUrl = await Task.Run(function: () => string.Format("{0}/{1}", cloudStorageAccount.BlobEndpoint.ToString().TrimEnd('/'), _appSettings.BlogPostContainer.TrimStart('/')));

            return staticContentUrl;
        }

        /// <summary>
        /// Convert string to int.
        /// </summary>
        /// <param name="input">input string instance.</param>
        /// <returns>Returns the <see cref="string"/></returns>
        private static int ConvertStringToInt(string input)
        {
            var result = int.TryParse(input, out int output);
            var converted = result ? output : 0;

            return converted;
        }

        /// <summary>
        /// Format date string for post.
        /// </summary>
        /// <param name="prefix">prefix string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        private static async Task<string> FormatDatePostedString(string prefix)
        {
            var year = prefix.Substring(0, 4);
            var month = GetMonthLong(prefix.Substring(5, 2));

            var day = prefix.Substring(8, 2);
            var datePostedString = await Task.Run(function: () => $"Posted { day } { month } { year }");

            return datePostedString;
        }

        /// <summary>
        /// Formats name string.
        /// </summary>
        /// <param name="input">input string instance.</param>
        /// <returns>Returns the <see cref="string"/></returns>
        private static string FormatName(string input)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(input);
        }

        /// <summary>
        /// Formats name string.
        /// </summary>
        /// <param name="input">input string instance.</param>
        /// <param name="prefix">prefix string instance.</param>
        /// <returns>Returns the <see cref="string"/></returns>
        private static string FormatName(string input, string prefix)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            var output = input
                .Replace(prefix, string.Empty)
                .Replace(".md", string.Empty);

            var formattedName = textInfo.ToTitleCase(output);

            return formattedName;
        }

        /// <summary>
        /// Gets <see cref="CloudStorageAccount"/> instance.
        /// </summary>
        /// <param name="storageAccountName">storageAccountName string instance.</param>
        /// <param name="storageAccountKey">storageAccountKey string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        private static CloudStorageAccount GetCloudStorageAccount(string storageAccountName, string storageAccountKey)
        {
            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            return cloudStorageAccount;
        }

        /// <summary>
        /// Get long month name.
        /// </summary>
        /// <param name="month">month string instance</param>
        /// <returns>Returns the <see cref="string"/></returns>
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

        /// <summary>
        /// Gets list of type <see cref="CloudBlockBlob"/>.
        /// </summary>
        /// <param name="container">container string instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        private async Task<List<CloudBlockBlob>> GetBlobList(string container)
        {
            var blobClient = await GetCloudBlobClientAsync();
            var containerReference = blobClient.GetContainerReference(container);

            var results = new List<IListBlobItem>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var response = await containerReference.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.All, 10, continuationToken, null, null);
                continuationToken = response.ContinuationToken;

                results.AddRange(response.Results);
            }
            while (continuationToken != null);

            var blobList = results
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .ToList();

            return blobList;
        }

        /// <summary>
        /// Get list of blog days.
        /// </summary>
        /// <param name="blobList">List of type <see cref="CloudBlockBlob"/></param>
        /// <returns>Returns the <see cref="Task"/></returns>
        private async Task<List<BlogPostDay>> GetBlogDays(List<CloudBlockBlob> blobList)
        {
            var posts = await GetBlogPosts(blobList);

            var days = blobList
                .GroupBy(x => x.Parent.Prefix)
                .Select(grp => grp.First())
                .ToList()
                .Select(selector: x => new BlogPostDay
                {
                    Prefix = x.Parent.Prefix,
                    Day = ConvertStringToInt(x.Name.Substring(8, 2)),
                    DayName = x.Name.Substring(8, 2),
                    BlogPostList = posts.Where(y => y.Prefix == x.Parent.Prefix).ToList()
                })
                .ToList();

            return await Task.Run(function: () => days);
        }

        /// <summary>
        /// Get list of blog months.
        /// </summary>
        /// <param name="blobList">List of type <see cref="CloudBlockBlob"/></param>
        /// <returns>Returns the <see cref="Task"/></returns>
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
                    BlogPostItems = items.Where(y => y.Prefix.Substring(0, 8) == x.Parent.Parent.Prefix).ToList()
                })
                .ToList();

            return await Task.Run(function: () => months);
        }

        /// <summary>
        /// Get list of blog post items.
        /// </summary>
        /// <param name="blobList">List of type <see cref="CloudBlockBlob"/></param>
        /// <returns>Returns the <see cref="Task"/></returns>
        private async Task<List<BlogPostItem>> GetBlogPosts(List<CloudBlockBlob> blobList)
        {
            var posts = blobList
                .Where(x => x.GetType() == typeof(CloudBlockBlob))
                .Select(x => (CloudBlockBlob)x)
                .ToList()
                .Select(selector: x => new BlogPostItem
                {
                    Prefix = x.Parent.Prefix,
                    Name = FormatName(x.Name, x.Parent.Prefix)
                })
                .ToList();

            return await Task.Run(function: () => posts);
        }

        /// <summary>
        /// Get list of type <see cref="BlogPostYear"/>.
        /// </summary>
        /// <param name="blobList">list of type <see cref="CloudBlockBlob"/></param>
        /// <returns>Returns the <see cref="Task"/></returns>
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

            return await Task.Run(function: () => years);
        }
    }
}