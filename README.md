# MaziwaPlus Backend (Scaffold)

This repository contains a scaffold for the MaziwaPlus Backend API — an ASP.NET Core Web API following the copilot instructions at the repo root.

Quick start (requires .NET 8 SDK):

```pwsh
cd src/MaziwaPlus.Api
dotnet restore
dotnet build
dotnet run
```

The project uses EF Core with SQL Server. Update `appsettings.json` `DefaultConnection` before running migrations.

Recommended local setup for Microsoft SQL Server and EF migrations:

1. Create/open the solution and restore packages:

```pwsh
# from repository root
dotnet new sln -n MaziwaPlus
dotnet sln add src/MaziwaPlus.Api/MaziwaPlus.Api.csproj
dotnet restore
```

2. Install the `dotnet-ef` tool (if not already installed) and add EF Design package (already added to csproj):

```pwsh
dotnet tool install --global dotnet-ef --version 8.0.0
```

3. Create the initial migration and update the database (ensure `DefaultConnection` in `appsettings.json` points to your SQL Server instance — example below):

```pwsh
cd src/MaziwaPlus.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Example SQL Server connection string for `appsettings.json`:

```json
"DefaultConnection": "Server=localhost;Database=MaziwaPlusDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
```

Notes:
- The project targets .NET 8. Use the .NET 8 SDK.
- If you prefer LocalDB for quick testing, use the existing `appsettings.json` connection string which points to `(localdb)\\mssqllocaldb`.

Quick script:

You can run the provided PowerShell helper to create migrations (if missing) and update the database. Run from repository root:

```pwsh
./scripts/Update-Database.ps1 -ProjectPath "src/MaziwaPlus.Api"
```

This script will install `dotnet-ef` globally if it's not present, create an `InitialCreate` migration when none exist, and run `dotnet ef database update`.

CI and tests
-----------

A GitHub Actions workflow is included at `.github/workflows/ci.yml` which builds the solution and applies EF migrations against a SQL Server service container.

Run unit tests locally:

```pwsh
cd src
dotnet test
```

Running the API locally
----------------------

You can run the API project directly with `dotnet run` by specifying the project path, or use the helper script.

From repository root:

```pwsh
# Run the API project directly
dotnet run --project src/MaziwaPlus.Api

# Or use the helper script (runs dotnet run in the API folder)
./scripts/Run-Api.ps1
```

**Important**: Before running the API, ensure migrations have been applied to create the database schema:

```pwsh
./scripts/Update-Database.ps1 -ProjectPath "src/MaziwaPlus.Api"
```

Accessing Swagger UI
--------------------

Once the API is running, Swagger/OpenAPI documentation is available at:

```
http://localhost:5000/swagger/ui
```

From the Swagger interface, you can:
- View all available endpoints
- Read endpoint documentation and parameters
- Try out endpoints directly with sample data (pre-populated by seed data)

**Note**: Swagger is only enabled in Development environment. For production, remove or conditionally disable it in `Program.cs`.

API Endpoints (Seed Data Included)
----------------------------------

The following sample data is automatically seeded on first run (Development only):
- **Farmers**: John Doe (Kisumu), Jane Smith (Nakuru), Peter Kipchoge (Eldoret)
- **Shops**: Main Dairy Store (Nairobi CBD), Healthy Milk Mart (Westlands), Fresh Milk Kiosk (Karen)
- **Collections**: 4 sample milk collections with various dates and quantities
- **Deliveries**: 3 sample deliveries (2 Accepted, 1 Pending)
- **Payments**: 2 sample payments (both Completed)

**POST /api/collections** — Add a new milk collection
```json
{
  "farmerId": 1,
  "collectionDate": "2024-01-15T10:30:00Z",
  "litersCollected": 50,
  "ratePerLiter": 2.50
}
```

**GET /api/collections/daily** — Get daily total milk collections
Query parameter: `date` (optional, defaults to today)
```
GET /api/collections/daily?date=2024-01-15
```

**POST /api/deliveries** — Record a milk delivery to a shop
```json
{
  "shopId": 1,
  "deliveryDate": "2024-01-15T14:00:00Z",
  "litersDelivered": 100
}
```

**POST /api/payments** — Record a payment for a delivery
```json
{
  "deliveryId": 1,
  "amountPaid": 250.00,
  "paymentDate": "2024-01-15T15:00:00Z"
}
```

**GET /api/farmers/{id}/summary** — Get farmer summary (total liters collected)
```
GET /api/farmers/1/summary
```

Example curl commands (once API is running):

```bash
# Add a milk collection
curl -X POST http://localhost:5000/api/collections \
  -H "Content-Type: application/json" \
  -d '{"farmerId":1,"collectionDate":"2024-01-15T10:30:00Z","litersCollected":50,"ratePerLiter":2.50}'

# Get daily collections
curl http://localhost:5000/api/collections/daily

# Record a delivery
curl -X POST http://localhost:5000/api/deliveries \
  -H "Content-Type: application/json" \
  -d '{"shopId":1,"deliveryDate":"2024-01-15T14:00:00Z","litersDelivered":100}'

# Record a payment
curl -X POST http://localhost:5000/api/payments \
  -H "Content-Type: application/json" \
  -d '{"deliveryId":1,"amountPaid":250.00,"paymentDate":"2024-01-15T15:00:00Z"}'

# Get farmer summary
curl http://localhost:5000/api/farmers/1/summary
```

Running migrations locally
-------------------------

Use the migration helper (creates migration if missing and applies it):

```pwsh
./scripts/Update-Database.ps1 -ProjectPath "src/MaziwaPlus.Api"
```

Notes:
- If you prefer a single command to restore/build/run the API from root:

```pwsh
dotnet restore
dotnet build
dotnet run --project src/MaziwaPlus.Api
```

- Ensure `appsettings.json` `DefaultConnection` points to a reachable SQL Server instance before running migrations or the API.

