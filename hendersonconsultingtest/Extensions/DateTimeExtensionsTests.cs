namespace HendersonConsulting.Web.Test.Extensions;

public class DateTimeExtensionsTests
{
    [Fact(DisplayName = "ToHumanReadableDateTime should return correct format")]
    public void ToHumanReadableDateTime_ShouldReturnCorrectFormat()
    {
        // Arrange
        long unixTimestamp = 1735689600000; // Corresponds to 2023-01-01 00:00:00

        // Act
        string result = unixTimestamp.ToHumanReadableDateTime();

        // Assert
        result.Should().Be("2025-01-01 00:00:00");
    }

    [Fact(DisplayName = "ToUnixTimeMillisecondsAtMidnight should return correct Unix timestamp")]
    public void ToUnixTimeMillisecondsAtMidnight_ShouldReturnCorrectUnixTimestamp()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;
        //var expectedUnixTimestamp = new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

        // Act
        var resultTimstamp = dateTime.ToUnixTimeMillisecondsAtMidnight();
        var resultDateTime = DateTimeOffset.FromUnixTimeMilliseconds(resultTimstamp).UtcDateTime;

        // Assert
        resultTimstamp.Should().Be(resultTimstamp);
        resultDateTime.Hour.Should().Be(0);
        resultDateTime.Minute.Should().Be(0);
        resultDateTime.Second.Should().Be(0);
        resultDateTime.Millisecond.Should().Be(0);
    }
}