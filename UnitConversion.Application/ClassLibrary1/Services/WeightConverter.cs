using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;

namespace UnitConversion.Application.Services
{
    public class WeightConverter : IUnitConverter<WeightUnit>
    {
        private readonly Dictionary<WeightUnit, double> _factors = new()
        {
            { WeightUnit.Kilograms, 1.0 },
            { WeightUnit.Pounds, 2.20462 },
            { WeightUnit.Grams, 0.001 }
        };

        public bool CanHandle(WeightUnit fromUnit, WeightUnit toUnit) =>
            _factors.ContainsKey(fromUnit) && _factors.ContainsKey(toUnit);

        public double Convert(double value, WeightUnit fromUnit, WeightUnit toUnit) =>
            value * (_factors[toUnit] / _factors[fromUnit]);
    }
}
