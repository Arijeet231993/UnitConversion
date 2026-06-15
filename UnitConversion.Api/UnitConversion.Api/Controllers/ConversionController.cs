using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;
using UnitConversion.Application.Services;

namespace UnitConversion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly LengthConverter _lengthConverter;
        private readonly TemperatureConverter _temperatureConverter;
        private readonly WeightConverter _weightConverter;

        private readonly ConversionRegistry _registry;

        public ConversionController(ConversionRegistry registry, LengthConverter lengthConverter,
            TemperatureConverter temperatureConverter, WeightConverter weightConverter)
        {
            _registry = registry;
            _lengthConverter = lengthConverter;
            _temperatureConverter = temperatureConverter;
            _weightConverter = weightConverter;
        }

        [HttpGet("length")]
        [SwaggerOperation(Tags = new[] { "Length Conversions" })]
        public ActionResult<ConversionResult> ConvertLength(
            [FromQuery(Name = "Input Value")] double value,
            [FromQuery(Name = "Source Unit")] LengthUnit fromUnit,
            [FromQuery(Name = "TargetUnit")] LengthUnit toUnit)
        {
            var result = _lengthConverter.Convert(value, fromUnit, toUnit);
            return Ok(new ConversionResult(result, toUnit.ToString()));
        }

        [HttpGet("temperature")]
        [SwaggerOperation(Tags = new[] { "Temperature Conversions" })]
        public ActionResult<ConversionResult> ConvertTemperature(
            [FromQuery(Name = "Input Value")] double value,
            [FromQuery(Name = "Source Unit")] TemperatureUnit fromUnit,
            [FromQuery(Name = "TargetUnit")] TemperatureUnit toUnit)
        {
            var result = _temperatureConverter.Convert(value, fromUnit, toUnit);
            return Ok(new ConversionResult(result, toUnit.ToString()));
        }

        [HttpGet("weight")]
        [SwaggerOperation(Tags = new[] { "Weight Conversions" })]
        public ActionResult<ConversionResult> ConvertWeight(
            [FromQuery(Name = "Input Value")] double value,
            [FromQuery(Name = "Source Unit")] WeightUnit fromUnit,
            [FromQuery(Name = "TargetUnit")] WeightUnit toUnit)
        {
            var result = _weightConverter.Convert(value, fromUnit, toUnit);
            return Ok(new ConversionResult(result, toUnit.ToString()));
        }
    }
}
