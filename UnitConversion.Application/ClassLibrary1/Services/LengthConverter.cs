using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Contracts;

namespace UnitConversion.Application.Services;

public class LengthConverter : IUnitConverter
{
    private readonly Dictionary<string, double> _factors = new()
    {
        { "meters", 1.0 },
        { "feet", 3.28084 },
        { "kilometers", 1000.0 }
    };

    public bool CanHandle(string fromUnit, string toUnit) =>
        _factors.ContainsKey(fromUnit) && _factors.ContainsKey(toUnit);

    public double Convert(double value, string fromUnit, string toUnit) =>
        value * (_factors[toUnit] / _factors[fromUnit]);
}
