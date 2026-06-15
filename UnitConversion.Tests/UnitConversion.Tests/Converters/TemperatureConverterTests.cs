using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;
using UnitConversion.Application.Services;
using Xunit;

public class TemperatureConverterTests
{
    private readonly TemperatureConverter _converter = new();

    [Theory]
    [InlineData(0, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit, 32)]
    [InlineData(100, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit, 212)]
    [InlineData(32, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius, 0)]
    [InlineData(212, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius, 100)]
    [InlineData(0, TemperatureUnit.Celsius, TemperatureUnit.Kelvin, 273.15)]
    [InlineData(273.15, TemperatureUnit.Kelvin, TemperatureUnit.Celsius, 0)]
    public void Convert_ValidConversions_ReturnsExpected(double value, TemperatureUnit from, TemperatureUnit to, double expected)
    {
        var result = _converter.Convert(value, from, to);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Convert_UnsupportedConversion_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _converter.Convert(100, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit));
    }
}
