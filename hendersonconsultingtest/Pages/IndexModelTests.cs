namespace HendersonConsulting.Web.Test.Pages;

public class IndexModelTests
{
    [Fact]
    public void GetYearsExpirenceValue_ReturnsCorrectValue()
    {
        // Arrange
        var indexModel = new IndexModel();

        // Act
        var result = indexModel.YearsExperience;

        // Assert
        result.Should().NotBe("0");
    }
}