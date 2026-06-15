using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.Controllers;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;
using UnitConversion.Application.Services;
using Xunit;

public class ConversionControllerTests
{
    private readonly ConversionController _controller;

    public ConversionControllerTests()
    {
        // Build registry with all converters
        var registry = new ConversionRegistry(
            new LengthConverter(),
            new TemperatureConverter(),
            new WeightConverter()
        );

        // Controller now takes ConversionRegistry and converters via DI
        _controller = new ConversionController(registry,
            new LengthConverter(),
            new TemperatureConverter(),
            new WeightConverter());
    }

    [Fact]
    public void Convert_MetersToFeet_ReturnsOkResult()
    {
        var result = _controller.ConvertLength(1, LengthUnit.Meters, LengthUnit.Feet);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var conversion = Assert.IsType<ConversionResult>(okResult.Value);

        Assert.Equal("Feet", conversion.Unit);   // Enum.ToString() returns "Feet"
        Assert.Equal(3.28084, conversion.Value, 5);
    }
}
