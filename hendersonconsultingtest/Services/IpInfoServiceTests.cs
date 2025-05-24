namespace HendersonConsulting.Web.Test.Services;

public class IpInfoServiceTests : TestBase
{
    private readonly ServiceProvider _serviceProvider;

    public IpInfoServiceTests() => _serviceProvider = InitiateServiceProvider();

    [Fact(DisplayName = "GetIpInfo should return a valid Location object")]
    public async Task GetIpInfoShouldReturnAValidLocationObject()
    {
        // Arrange
        var ipAddress = "69.215.26.194";
        var service = _serviceProvider.GetService<IIpInfoService>()!;

        // Act
        var result = await service.GetLocationByIpAddressAsync(ipAddress);

        // Assert
        result.Should().NotBeNull("the result should not be null.");
        result.City.Should().NotBeNullOrEmpty("the city should not be null or empty.");
    }
}