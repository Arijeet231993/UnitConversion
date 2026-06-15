using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Enums;

namespace UnitConversion.Application.Contracts;

public record LengthConversionRequest(double Value, LengthUnit FromUnit, LengthUnit ToUnit);
public record TemperatureConversionRequest(double Value, TemperatureUnit FromUnit, TemperatureUnit ToUnit);
public record WeightConversionRequest(double Value, WeightUnit FromUnit, WeightUnit ToUnit);

public record ConversionResult(double Value, string Unit);

public interface IUnitConverter<TUnit>
{
    bool CanHandle(TUnit fromUnit, TUnit toUnit);
    double Convert(double value, TUnit fromUnit, TUnit toUnit);
}
