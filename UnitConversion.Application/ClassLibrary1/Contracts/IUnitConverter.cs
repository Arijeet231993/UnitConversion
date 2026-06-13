using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConversion.Application.Contracts;

public record ConversionRequest(double Value, string FromUnit, string ToUnit);
public record ConversionResult(double Value, string Unit);

public interface IUnitConverter
{
    bool CanHandle(string fromUnit, string toUnit);
    double Convert(double value, string fromUnit, string toUnit);
}
