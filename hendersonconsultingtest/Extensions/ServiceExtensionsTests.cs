namespace HendersonConsulting.Web.Test.Extensions;

public class ServiceExtensionsTests : TestBase
{
    [Fact(DisplayName = "ServiceExtensions should be registered")]
    public void ServiceExtensions_AreRegistered()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.RegisterAzureKeyVault();

        // Act
        ServiceExtensions.RegisterScopedServices(builder.Services);
        ServiceExtensions.RegisterHttpClients(builder.Services, builder.Configuration);
        ServiceExtensions.RegisterApplicationSettings(builder.Services, builder.Configuration);

        var serviceProvider = builder.Services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IIpInfoService>());
        Assert.NotNull(serviceProvider.GetService<IEpochService>());
        Assert.NotNull(serviceProvider.GetService<IBuildInfoService>());

        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        Assert.NotNull(httpClientFactory);

        var ipApiHttpClient = httpClientFactory.CreateClient(Constants.ApplicationConstants.IpApiHttpClient);
        Assert.NotNull(ipApiHttpClient);

        var googleRecaptchaHttpClient = httpClientFactory.CreateClient(Constants.ApplicationConstants.GoogleRecaptchaHttpClient);
        Assert.NotNull(googleRecaptchaHttpClient);
    }
}