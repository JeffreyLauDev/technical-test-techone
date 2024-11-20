using Xunit;

public class NumberConverterTests
{
    private readonly NumberConverter _converter;

    public NumberConverterTests()
    {
        _converter = new NumberConverter();
    }

    [Fact]
    public void NumberToWords_ShouldReturnZeroDollars_ForZeroInput()
    {
        var result = _converter.NumberToWords(0);
        Assert.Equal("ZERO DOLLARS", result);
    }

    [Theory]
    [InlineData(123.45, "ONE HUNDRED TWENTY THREE DOLLARS AND FORTY FIVE CENTS")]
    [InlineData(1000, "ONE THOUSAND DOLLARS")]
    [InlineData(0.99, "ZERO DOLLARS AND NINETY NINE CENTS")]
    public void NumberToWords_ShouldReturnExpectedResult(decimal input, string expected)
    {
        var result = _converter.NumberToWords(input);
        Assert.Equal(expected, result);
    }
}
