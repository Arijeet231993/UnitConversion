using Microsoft.AspNetCore.Mvc;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;

namespace UnitConversion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly ConversionRegistry _registry;

        public ConversionController(ConversionRegistry registry)
        {
            _registry = registry;
        }

        /// <summary>
        /// Converts a value from one unit to another.
        /// </summary>
        /// <param name="request">Conversion request containing value, fromUnit, and toUnit.</param>
        /// <returns>Converted value and target unit.</returns>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(ConversionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ConversionResult> Convert([FromBody] ConversionRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            try
            {
                var result = _registry.Convert(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
