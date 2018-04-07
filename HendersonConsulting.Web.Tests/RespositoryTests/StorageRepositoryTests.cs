using HendersonConsulting.Web.Models;
using HendersonConsulting.Web.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HendersonConsulting.Web.Tests.RespositoryTests
{
    public class StorageRepositoryTests
    {
        private readonly Mock<IOptions<AppSettings>> _iOptions;

        public StorageRepositoryTests()
        {
            _iOptions = new Mock<IOptions<AppSettings>>();
            _iOptions.Setup(x => x.Value).Returns(new AppSettings
            {
                StorageAccountKey = "VoNHODPRPhx+5wHM5sCba038nbsS3fhQe8yzkXB+4dR7Lz+oF04SKvXiH+K048OXzFyHFkhNO15IzCODimsvJQ==",
                StorageAccountName = "hendersonconsulting",
                BlogPostContainer = "henderson-consulting-posts"
            });
        }

        [Fact(DisplayName = "GetPostItemsAsync should return a list of type PostYears")]
        [Trait("Category", "StorageRepositoryTests" )]
        public async Task GetBlogPostsAsyncReturnsList()
        { 
            // Arrange
            var storageRepository = new StorageRepository(_iOptions.Object);

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
            var storageRepository = new StorageRepository(_iOptions.Object);

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
            var storageRepository = new StorageRepository(_iOptions.Object);

            // Act
            var sut = await storageRepository.GetDefaultPostItemAsync();

            // Assert
            Assert.IsType<BlogPostContent>(sut);
        }

        [Fact(DisplayName = "GetStaticContentBaseUrl should return a string")]
        [Trait("Category", "StorageRepositoryTests")]
        public async Task GetStaticContentBaseUrlAsyncReturnsString()
        {
            // Arrange
            var storageRepository = new StorageRepository(_iOptions.Object);

            // Act
            var sut = await storageRepository.GetStaticContentBaseUrlAsync();

            // Assert
            Assert.IsType<string>(sut);
        }

    }
}
