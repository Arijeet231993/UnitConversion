using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.Controllers;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Services;
using Xunit;

public class ConversionControllerTests
{
    private readonly ConversionController _controller;

    public ConversionControllerTests()
    {
        var registry = new ConversionRegistry(new IUnitConverter[]
        {
            new LengthConverter()
        });
        _controller = new ConversionController(registry);
    }

    [Fact]
    public void Convert_MetersToFeet_ReturnsOkResult()
    {
        var request = new ConversionRequest(1, "meters", "feet");
        var result = _controller.Convert(request);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var conversion = Assert.IsType<ConversionResult>(okResult.Value);
        Assert.Equal("feet", conversion.Unit);
        Assert.Equal(3.28084, conversion.Value, 5);
    }
}
