using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;

namespace UnitConversion.Application.Services
{
    public class LengthConverter : IUnitConverter<LengthUnit>
    {
        private readonly Dictionary<LengthUnit, double> _factors = new()
        {
            { LengthUnit.Meters, 1.0 },
            { LengthUnit.Feet, 3.28084 },
            { LengthUnit.Kilometers, 1000.0 },
            { LengthUnit.Miles, 1609.34 }
        };

        public bool CanHandle(LengthUnit fromUnit, LengthUnit toUnit) =>
            _factors.ContainsKey(fromUnit) && _factors.ContainsKey(toUnit);

        public double Convert(double value, LengthUnit fromUnit, LengthUnit toUnit) =>
            value * (_factors[toUnit] / _factors[fromUnit]);
    }
}