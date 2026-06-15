using UnitConversion.Application.Contracts;
using UnitConversion.Application.Enums;

namespace UnitConversion.Application.Services
{
    public class TemperatureConverter : IUnitConverter<TemperatureUnit>
    {
        public bool CanHandle(TemperatureUnit fromUnit, TemperatureUnit toUnit) => true;

        public double Convert(double value, TemperatureUnit fromUnit, TemperatureUnit toUnit)
        {
            return (fromUnit, toUnit) switch
            {
                (TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit) => (value * 9 / 5) + 32,
                (TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius) => (value - 32) * 5 / 9,
                (TemperatureUnit.Celsius, TemperatureUnit.Kelvin) => value + 273.15,
                (TemperatureUnit.Kelvin, TemperatureUnit.Celsius) => value - 273.15,
                _ => throw new InvalidOperationException("Unsupported conversion")
            };
        }
    }
}
