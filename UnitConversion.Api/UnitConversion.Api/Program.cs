using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models; // ✅ Correct namespace
using System.Text.Json.Serialization;
using UnitConversion.Application;
using UnitConversion.Application.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Register converters and registry
builder.Services.AddScoped<LengthConverter>();
builder.Services.AddScoped<TemperatureConverter>();
builder.Services.AddScoped<WeightConverter>();
builder.Services.AddScoped<ConversionRegistry>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Unit Conversion API",
        Version = "1.0",
        Description = "API for converting values between units (length, temperature, weight)."
    });

    options.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Conversion API v1");
        c.RoutePrefix = "swagger"; // UI available at /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
