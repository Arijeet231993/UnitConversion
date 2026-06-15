using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

namespace UnitConversion.Api.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum && schema is OpenApiSchema openApiSchema)
            {
                openApiSchema.Type = JsonSchemaType.String;
                openApiSchema.Format = null;
                openApiSchema.Enum = Enum.GetNames(context.Type)
                    .Select(name => (JsonNode)JsonValue.Create(name)!)
                    .ToList();
            }
        }
    }
}
