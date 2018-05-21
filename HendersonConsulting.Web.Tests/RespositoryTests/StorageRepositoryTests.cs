namespace HendersonConsulting.Web.Tests.RespositoryTests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Models;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Moq;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="StorageRepository"/> class.
    /// </summary>
    public class StorageRepositoryTests
    {
        /// <summary>
        /// <see cref="AppSettings"/> mocked instance.
        /// </summary>
        private readonly Mock<IOptions<AppSettings>> _appSettingsOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageRepositoryTests" /> class.
        /// </summary>
        public StorageRepositoryTests()
        {
            _appSettingsOptions = new Mock<IOptions<AppSettings>>();
            _appSettingsOptions.Setup(x => x.Value).Returns(new AppSettings
            {
                StorageAccountKey = "<StorageAccountKey>",
                StorageAccountName = "hendersonconsulting",
                BlogPostContainer = "henderson-consulting-posts",
                ImagesContainer = "henderson-consulting-images",
                StaticContainer = "henderson-consulting-static"
            });
        }

        [Fact(DisplayName = "GetPostItemsAsync should return a list of type PostYears")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetBlogPostsAsyncReturnsList()
        { 
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetBlogPostListAsync();

            // Assert
            Assert.IsType<List<BlogPostYear>>(sut);
        }

        [Fact(DisplayName = "GetCloudBlobClient should return a an instance of a CloudBlobClient")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetCloudBlobClientAsyncReturnsCloudBlobClient()
        {
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetCloudBlobClientAsync();

            // Assert
            Assert.IsType<CloudBlobClient>(sut);
        }

        [Fact(DisplayName = "GetDefaultPostItemAsync should return a CloudBlobClient")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetDefaultPostItemAsyncReturnsCloudBlobClient()
        {
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetDefaultPostItemAsync();

            // Assert
            Assert.IsType<BlogPostContent>(sut);
        }

        [Fact(DisplayName = "GetBlogPostItemAsync should return a CloudBlobClient")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetBlogAsyncPostItemReturnsCloudBlobClient()
        {
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            var year = "2018";
            var month = "04";
            var day = "07";
            var name = "reboot";

            // Act
            var sut = await storageRepository.GetBlogPostItemAsync(year, month, day, name);

            // Assert
            Assert.IsType<BlogPostContent>(sut);
        }

        [Fact(DisplayName = "GetStaticContentBaseUrl should return a string")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetStaticContentBaseUrlAsyncReturnsString()
        {
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetStaticContentBaseUrlAsync();

            // Assert
            Assert.IsType<string>(sut);
        }

        [Fact(DisplayName = "GetImageBlobAsych should return a CloudBlockBlob")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetImageBlobAsychReturnsCloudBlockBlob()
        {
            // Arrange
            var itemPath = "images/test.png";
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetImageBlobAsych(itemPath);

            // Assert
            Assert.IsType<CloudBlockBlob>(sut);
        }

        [Fact(DisplayName = "GetCategoriesAsync should return a list of type Category")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetCategoriesAsyncReturnsList()
        {
            // Arrange
            var storageRepository = new StorageRepository(_appSettingsOptions.Object);

            // Act
            var sut = await storageRepository.GetCategoriesAsync();

            // Assert
            Assert.IsType<List<Category>>(sut);
        }
    }
}
