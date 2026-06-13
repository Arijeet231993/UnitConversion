using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnitConversion.Application.Services;
using Xunit;

public class LengthConverterTests
{
    private readonly LengthConverter _converter = new();

    [Fact]
    public void Convert_MetersToFeet_ReturnsCorrectValue()
    {
        var result = _converter.Convert(1, "meters", "feet");
        Assert.Equal(3.28084, result, 5);
    }

    [Theory]
    [InlineData(0, "meters", "feet", 0)]
    [InlineData(1000, "meters", "kilometers", 1)]
    public void Convert_ValidConversions_ReturnsExpected(double value, string from, string to, double expected)
    {
        var result = _converter.Convert(value, from, to);
        Assert.Equal(expected, result, 5);
    }
}

