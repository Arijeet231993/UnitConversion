using Microsoft.OpenApi.Models;
using UnitConversion.Application;
using UnitConversion.Application.Contracts;
using UnitConversion.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitConverter, LengthConverter>();
builder.Services.AddScoped<ConversionRegistry>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Unit Conversion API",
        Version = "v1",
        Description = "API for converting values between units (length, temperature, weight)."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Conversion API v1");
        // IMPORTANT: leave RoutePrefix default so Swagger is served at /swagger
    });
}

app.MapControllers();
app.Run();
