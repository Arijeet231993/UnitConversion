using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;
using UnitConversion.Application.Services;
using Xunit;
using Xunit;

public class LengthConverterTests
{
    private readonly LengthConverter _converter = new();

    [Fact]
    public void Convert_MetersToFeet_ReturnsCorrectValue()
    {
        var result = _converter.Convert(1, LengthUnit.Meters, LengthUnit.Feet);
        Assert.Equal(3.28084, result, 5);
    }

    [Theory]
    [InlineData(0, LengthUnit.Meters, LengthUnit.Feet, 0)]
    [InlineData(1000, LengthUnit.Meters, LengthUnit.Kilometers, 1)]
    [InlineData(1609.34, LengthUnit.Meters, LengthUnit.Miles, 1)]
    [InlineData(3.28084, LengthUnit.Feet, LengthUnit.Meters, 1)]
    public void Convert_ValidConversions_ReturnsExpected(double value, LengthUnit from, LengthUnit to, double expected)
    {
        var result = _converter.Convert(value, from, to);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Convert_UnsupportedConversion_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _converter.Convert(1, LengthUnit.Miles, LengthUnit.Kilometers));
    }
}


