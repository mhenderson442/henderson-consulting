namespace HendersonConsulting.Web.Test.Services;

public class EpochServiceTests : TestBase
{
    private readonly ServiceProvider _serviceProvider;

    public EpochServiceTests() => _serviceProvider = InitiateServiceProvider();

    [Fact(DisplayName = "EpochService should be registered")]
    public void EpochService_IsRegistered()
    {
        // Arrange & Act
        var service = _serviceProvider.GetService<IEpochService>();

        // Assert
        Assert.NotNull(service);
    }

    [Fact(DisplayName = "GetCurrentEpoch should return a Unix timestamp")]
    public void GetCurrentEpoch_ReturnsUnixTimestamp()
    {
        // Arrange
        var service = _serviceProvider.GetService<IEpochService>()!;

        // Act
        var result = service.GetCurrentEpoch();

        // Assert
        result.Should().BeGreaterThan(0, "the result should be a positive Unix timestamp.");
        result.ToString().Length.Should().Be(13, "the result should be a Unix timestamp in milliseconds.");
    }

    [Fact(DisplayName = "GetCurrentEpoch should return as Unix timstamp at midnight")]
    public void GetCurrentEpoch_AtMidnight()
    {
        // Arrange
        var service = _serviceProvider.GetService<IEpochService>()!;
        var midnight = new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeMilliseconds();

        // Act
        var result = service.GetCurrentEpochAtMidnight();

        // Assert
        result.Should().Be(midnight, "the result should be the Unix timestamp for midnight UTC.");
    }

    [Fact(DisplayName = "GetCurrentEpoch should return as Unix timstamp as a human readable string")]
    public void GetCurrentEpoch_AsHumanReadableString()
    {
        // Arrange
        var service = _serviceProvider.GetService<IEpochService>()!;
        var currentEpoch = service.GetCurrentEpoch();
        var expectedDateTime = DateTimeOffset.FromUnixTimeMilliseconds(currentEpoch).DateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // Act
        var result = currentEpoch.ToHumanReadableDateTime();

        // Assert
        result.Should().Be(expectedDateTime, "the result should be the human-readable date time format of the Unix timestamp.");
    }

    [Fact(DisplayName = "ConvertToUnixTimeMilliseconds should return a Unix timestamp")]
    public void ConvertToUnixTimeMilliseconds_ReturnsUnixTimestamp()
    {
        // Arrange
        var service = _serviceProvider.GetService<IEpochService>()!;
        var dateTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Act
        var result = service.ConvertToUnixTimeMilliseconds(dateTime);

        // Assert
        result.Should().Be(1735689600000, "the result should be the Unix timestamp for the given date.");
    }
}