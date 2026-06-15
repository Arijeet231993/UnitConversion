using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Services;

namespace UnitConversion.Application
{
    public class ConversionRegistry
    {
        private readonly LengthConverter _lengthConverter;
        private readonly TemperatureConverter _temperatureConverter;
        private readonly WeightConverter _weightConverter;

        public ConversionRegistry(
            LengthConverter lengthConverter,
            TemperatureConverter temperatureConverter,
            WeightConverter weightConverter)
        {
            _lengthConverter = lengthConverter;
            _temperatureConverter = temperatureConverter;
            _weightConverter = weightConverter;
        }

        public ConversionResult Convert(LengthConversionRequest request)
        {
            if (!_lengthConverter.CanHandle(request.FromUnit, request.ToUnit))
                throw new InvalidOperationException("Unsupported length conversion");

            var result = _lengthConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return new ConversionResult(result, request.ToUnit.ToString());
        }

        public ConversionResult Convert(TemperatureConversionRequest request)
        {
            if (!_temperatureConverter.CanHandle(request.FromUnit, request.ToUnit))
                throw new InvalidOperationException("Unsupported temperature conversion");

            var result = _temperatureConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return new ConversionResult(result, request.ToUnit.ToString());
        }

        public ConversionResult Convert(WeightConversionRequest request)
        {
            if (!_weightConverter.CanHandle(request.FromUnit, request.ToUnit))
                throw new InvalidOperationException("Unsupported weight conversion");

            var result = _weightConverter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return new ConversionResult(result, request.ToUnit.ToString());
        }
    }
}


