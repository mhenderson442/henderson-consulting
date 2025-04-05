using HendersonConsulting.Web.Pages;
using Moq;

namespace HendersonConsulting.Web.Test.Pages;

public class EpochTimesTests
{
    private readonly EpochTimesModel _indexModel;

    public EpochTimesTests()
    {
        var epochService = new EpochService();
        var currentEpoch = epochService.GetCurrentEpoch();

        _indexModel = new EpochTimesModel(epochService)
        {
            CurrentEpochTime = currentEpoch,
            CurrentEpochTimeString = currentEpoch.ToHumanReadableDateTime(),
        };
    }

    [Fact(DisplayName = "CurrentEpochTime property should be of type long.")]
    public void CurrentEpochTimeReturnsLong()
    {
        // Arrange

        // Act
        var result = _indexModel.CurrentEpochTime;

        // Assert
        result.Should().BeOfType(typeof(long));
    }

    [Fact(DisplayName = "CurrentEpochTimeString property should be of type string.")]
    public void CurrentEpochTimeReturnsString()
    {
        // Arrange

        // Act
        var result = _indexModel.CurrentEpochTimeString;

        // Assert
        result.Should().BeOfType<string>();
    }

    [Fact(DisplayName = "CurrentEpochTimeString property should be formatted as a human readable timestamp.")]
    public void CurrentEpochTimeStringIsFormattedCorrectly()
    {
        // Arrange
        var expectedFormat = "yyyy-MM-dd HH:mm:ss"; // Example format

        // Act
        var result = _indexModel.CurrentEpochTimeString;
        var isValidFormat = DateTime.TryParseExact(result, expectedFormat, null, System.Globalization.DateTimeStyles.None, out _);

        // Assert
        isValidFormat.Should().BeTrue("because the CurrentEpochTimeString should be in a human readable timestamp format");
    }

    [Fact(DisplayName = "Form submission should set selected Unix timestamp.")]
    public void FormSubmissionUpdatesProperties()
    {
        // Arrange
        var dateTimeInput = DateTime.UtcNow;

        // Act
        _indexModel.OnPost(dateTimeInput);

        // Assert
        _indexModel.ConvertedTimestamp.Should().NotBeNull();
    }
}