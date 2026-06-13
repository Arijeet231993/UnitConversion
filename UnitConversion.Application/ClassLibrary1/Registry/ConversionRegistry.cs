using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConversion.Application.Contracts;

namespace UnitConversion.Application;
public class ConversionRegistry
{
    private readonly IEnumerable<IUnitConverter> _converters;
    public ConversionRegistry(IEnumerable<IUnitConverter> converters) => _converters = converters;

    public ConversionResult Convert(ConversionRequest request)
    {
        var converter = _converters.FirstOrDefault(c => c.CanHandle(request.FromUnit, request.ToUnit));
        if (converter == null) throw new InvalidOperationException("Unsupported conversion");
        return new ConversionResult(converter.Convert(request.Value, request.FromUnit, request.ToUnit), request.ToUnit);
    }
}

