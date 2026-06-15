using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;
using UnitConversion.Application.Services;
using Xunit;

public class WeightConverterTests
{
    private readonly WeightConverter _converter = new();

    [Theory]
    [InlineData(1, WeightUnit.Kilograms, WeightUnit.Pounds, 2.20462)]
    [InlineData(2.20462, WeightUnit.Pounds, WeightUnit.Kilograms, 1)]
    [InlineData(1000, WeightUnit.Grams, WeightUnit.Kilograms, 1)]
    [InlineData(1, WeightUnit.Kilograms, WeightUnit.Grams, 1000)]
    public void Convert_ValidConversions_ReturnsExpected(double value, WeightUnit from, WeightUnit to, double expected)
    {
        var result = _converter.Convert(value, from, to);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Convert_UnsupportedConversion_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _converter.Convert(1, WeightUnit.Grams, WeightUnit.Pounds));
    }
}
