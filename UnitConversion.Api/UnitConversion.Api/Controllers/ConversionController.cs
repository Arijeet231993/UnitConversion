using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Services;

namespace UnitConversion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly LengthConverter _lengthConverter = new();
        private readonly TemperatureConverter _temperatureConverter = new();
        private readonly WeightConverter _weightConverter = new();

        private readonly ConversionRegistry _registry;

        // ✅ Constructor injection
        public ConversionController(ConversionRegistry registry)
        {
            _registry = registry;
        }

        [HttpPost("length")]
        [SwaggerOperation(Tags = new[] { "Length Conversions" })]
        public ActionResult<ConversionResult> ConvertLength([FromBody] LengthConversionRequest request)
        {
            var result = _lengthConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return Ok(new ConversionResult(result, request.ToUnit.ToString()));
        }

        [HttpPost("temperature")]
        [SwaggerOperation(Tags = new[] { "Temperature Conversions" })]
        public ActionResult<ConversionResult> ConvertTemperature([FromBody] TemperatureConversionRequest request)
        {
            var result = _temperatureConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return Ok(new ConversionResult(result, request.ToUnit.ToString()));
        }

        [HttpPost("weight")]
        [SwaggerOperation(Tags = new[] { "Weight Conversions" })]
        public ActionResult<ConversionResult> ConvertWeight([FromBody] WeightConversionRequest request)
        {
            var result = _weightConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return Ok(new ConversionResult(result, request.ToUnit.ToString()));
        }
    }
}
