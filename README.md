# Unit Conversion API

A clean, layered ASP.NET Core 8 Web API for converting values across three unit categories — **Length**, **Temperature**, and **Weight**. The API is fully documented via Swagger UI, where unit fields are rendered as dropdowns (no manual JSON input required), making it immediately usable from the browser without any external tooling.

The solution is organised into three projects:

| Project | Responsibility |
|---|---|
| `UnitConversion.Api` | ASP.NET Core Web API — controllers, Swagger configuration, DI wiring |
| `UnitConversion.Application` | Domain logic — converters, enums, contracts, registry |
| `UnitConversion.Tests` | xUnit unit tests covering converters and controller actions |

Three conversion endpoints are exposed:

- `GET /api/Conversion/length` — Meters, Feet, Kilometers, Miles
- `GET /api/Conversion/temperature` — Celsius, Fahrenheit, Kelvin
- `GET /api/Conversion/weight` — Kilograms, Pounds, Grams

Each endpoint accepts **Input Value** (numeric), **Source Unit** (dropdown), and **TargetUnit** (dropdown) as query parameters and returns the converted value with its unit label.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- (Optional) [Docker Desktop](https://www.docker.com/products/docker-desktop) for containerised runs
- (Optional) Visual Studio 2022+ or VS Code

---

## Running Locally

### 1. Clone the repository

```bash
git clone https://github.com/Arijeet231993/UnitConversion.git
cd UnitConversion
```

### 2. Run with the .NET CLI

```bash
cd UnitConversion.Api/UnitConversion.Api
dotnet run
```

The API starts on **http://localhost:5000** (HTTP) and **https://localhost:5001** (HTTPS).  
Open **http://localhost:5000/swagger** in your browser to access the Swagger UI.

### 3. Run with Visual Studio

1. Open `UnitConversion.sln`.
2. Set `UnitConversion.Api` as the startup project.
3. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging).  
   The browser will launch directly to `/swagger`.

### 4. Run with Docker

```bash
cd UnitConversion.Api/UnitConversion.Api
docker build -t unitconversion-api .
docker run -p 8080:8080 unitconversion-api
```

Then open **http://localhost:8080/swagger**.

---

## Running Tests

```bash
dotnet test
```

Or in Visual Studio, open **Test Explorer** and click **Run All**.

---

## API Usage Example

```
GET /api/Conversion/length?Input Value=100&Source Unit=Meters&TargetUnit=Feet
```

**Response:**

```json
{
  "value": 328.084,
  "unit": "Feet"
}
```

---

## Design Decisions & Tradeoffs

### Query parameters over request body
All endpoints use `[FromQuery]` individual parameters rather than a `[FromBody]` JSON object. This is a deliberate UX decision: Swagger UI renders enum query parameters as native `<select>` dropdowns, so users can interact with the API entirely through the browser without writing JSON. The tradeoff is that the query string is slightly more verbose for programmatic callers, but the gain in discoverability outweighs this for a utility API.

### EnumSchemaFilter for Swashbuckle 10.x
Swashbuckle 10.x targets Microsoft.OpenApi 2.x, which introduced breaking API changes (`IOpenApiSchema` interface with read-only properties, `JsonSchemaType` enum instead of a string, removal of `Microsoft.OpenApi.Models` namespace). A custom `EnumSchemaFilter` was implemented to correctly emit `type: "string"` with named enum values so that Swagger UI renders dropdowns rather than free-text boxes.

### Layered architecture with a Registry pattern
Business logic lives entirely in `UnitConversion.Application`, keeping the API layer thin. A `ConversionRegistry` acts as a single dispatch point for all conversion types, making it straightforward to add new categories (e.g. volume, speed) without touching the API layer. Each converter implements `IUnitConverter<TUnit>`, making the system open for extension.

### JsonStringEnumConverter
Enums are serialised as strings throughout (both in request binding and response payloads), improving readability and forward compatibility when consuming the API from clients that may not share the same enum definitions.

### No database / persistence
This is a stateless calculation API. All conversion factors are held in-memory dictionaries within each converter. This is intentional — there is no mutable state to persist, and it keeps the deployment footprint minimal (no connection strings, no migrations).
