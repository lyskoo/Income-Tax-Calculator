using Business.Validations;

namespace Tests.BusinessTests;

public class ValidationTests
{
    [Theory]
    [InlineData("0")]
    [InlineData("10")]
    [InlineData("100")]
    public void ValidateNumber_ValidNumbers_ReturnsTrue(string number)
    {
        decimal numberDecimal = decimal.Parse(number);
        bool isValid = Validation.ValidateNumber(numberDecimal);

        Assert.True(isValid);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("-10")]
    public void ValidateNumber_InvalidNumbers_ReturnsFalse(string number)
    {
        decimal? numberDecimal = decimal.Parse(number);

        bool isValid = Validation.ValidateNumber(numberDecimal);

        Assert.False(isValid);
    }
}
