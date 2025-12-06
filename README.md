# MaziwaPlus Backend API

A robust **ASP.NET Core 8 Web API** for managing milk collection, delivery, and payment operations in the MaziwaPlus milk management system. Built with clean architecture principles, Entity Framework Core, and comprehensive REST endpoints.

## Overview

MaziwaPlus Backend is a production-ready API that enables farmers, dairy shops, and payment processors to efficiently manage milk supply chain operations. The system tracks milk collections from farmers, deliveries to shops, and payments with comprehensive business logic validation.

**Key Features:**
- 🚚 **Milk Collection Tracking** — Record farmer milk collections with automatic cost calculations
- 📦 **Delivery Management** — Track milk deliveries to shops with status workflows
- 💳 **Payment Processing** — Manage delivery payments with payment status tracking
- 👨‍🌾 **Farmer Analytics** — Get farmer summaries including total liters collected
- 📊 **Real-time Aggregation** — Daily milk collection totals and statistics
- 🔍 **RESTful API** — Clean, documented endpoints with Swagger UI
- 🛡️ **Data Validation** — Business rule validation at service layer
- 📱 **Async/Await** — Non-blocking I/O for optimal performance
- 🧪 **Unit Tested** — 11 comprehensive xUnit tests with Moq mocks
- 🔄 **CI/CD Ready** — GitHub Actions workflow with SQL Server integration testing

## Tech Stack

- **Framework**: ASP.NET Core 8.0 (.NET 8)
- **Language**: C# 12 with nullable reference types
- **Database**: Microsoft SQL Server with Entity Framework Core 8.0.0
- **Architecture**: Clean Architecture (Domain/Data/Api layers)
- **Testing**: xUnit 2.4.2 + Moq 4.20.0
- **Documentation**: Swagger/OpenAPI UI
- **CI/CD**: GitHub Actions with SQL Server service container

## Quick Start

Requirements: **.NET 8 SDK**

```pwsh
cd src/MaziwaPlus.Api
dotnet restore
dotnet build
dotnet run
```

The API will start on `http://localhost:5000` with Swagger UI at the root path.

## Database Setup

The project uses Microsoft SQL Server with Entity Framework Core. Update `appsettings.json` before running migrations.

### Step 1: Install EF Core Tools

```pwsh
dotnet tool install --global dotnet-ef --version 8.0.0
```

### Step 2: Configure Connection String

Edit `appsettings.json` and update the `DefaultConnection`:

**Option A: LocalDB (Quick Testing)**
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MaziwaPlusDb;Integrated Security=true;"
```

**Option B: SQL Server Instance**
```json
"DefaultConnection": "Server=localhost;Database=MaziwaPlusDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
```

### Step 3: Apply Migrations

```pwsh
cd src/MaziwaPlus.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Or use the helper script:

```pwsh
./scripts/Update-Database.ps1 -ProjectPath "src/MaziwaPlus.Api"
```

## API Documentation

Once running, access **Swagger UI** at: `http://localhost:5000/`

### Available Endpoints

#### Collections
- **POST** `/api/collections` — Add a new milk collection
  ```json
  {
    "farmerId": 1,
    "collectionDate": "2024-01-15T10:30:00Z",
    "litersCollected": 50,
    "ratePerLiter": 2.50
  }
  ```

- **GET** `/api/collections/daily` — Get daily total milk collections
  ```
  GET /api/collections/daily?date=2024-01-15
  ```

#### Deliveries
- **POST** `/api/deliveries` — Record a milk delivery to a shop
  ```json
  {
    "shopId": 1,
    "deliveryDate": "2024-01-15T14:00:00Z",
    "litersDelivered": 100
  }
  ```

#### Payments
- **POST** `/api/payments` — Record a payment for a delivery
  ```json
  {
    "deliveryId": 1,
    "amountPaid": 250.00,
    "paymentDate": "2024-01-15T15:00:00Z"
  }
  ```

#### Farmers
- **GET** `/api/farmers/{id}/summary` — Get farmer summary (total liters collected)
  ```
  GET /api/farmers/1/summary
  ```

### Sample curl Commands

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

## Sample Data

The database is automatically seeded with sample data on first run (Development environment only):

- **3 Farmers**: John Doe (Kisumu), Jane Smith (Nakuru), Peter Kipchoge (Eldoret)
- **3 Shops**: Main Dairy Store (Nairobi CBD), Healthy Milk Mart (Westlands), Fresh Milk Kiosk (Karen)
- **4 Milk Collections**: Various dates with realistic quantities
- **3 Deliveries**: Mix of accepted and pending status
- **2 Payments**: Sample completed payments

Use Swagger UI to explore and test all endpoints with this pre-populated data.

## Project Structure

```
src/
├── MaziwaPlus.Domain/        # Domain entities (no dependencies)
│   └── Entities/
│       ├── Farmer.cs
│       ├── MilkCollection.cs
│       ├── Shop.cs
│       ├── Delivery.cs
│       └── Payment.cs
├── MaziwaPlus.Data/          # Data access layer
│   ├── MaziwaPlusContext.cs
│   └── Repositories/
│       ├── IRepository.cs
│       └── EfRepository.cs
├── MaziwaPlus.Api/           # API layer
│   ├── Controllers/
│   ├── Services/
│   ├── Dtos/
│   └── Program.cs
└── MaziwaPlus.Tests/         # Unit tests
    └── *ServiceTests.cs

MaziwaPlus.sln               # Solution file
```

## Testing

Run all unit tests:

```pwsh
dotnet test
```

Run specific test file:

```pwsh
dotnet test --filter "CollectionServiceTests"
```

The test suite includes:
- **CollectionService Tests** (2) — Collection creation and daily aggregation
- **DeliveryService Tests** (3) — Delivery validation and processing
- **PaymentService Tests** (3) — Payment validation and processing
- **FarmerService Tests** (3) — Farmer summary and analytics

All tests use Moq for isolation and xUnit for assertions.

## CI/CD

GitHub Actions workflow (`.github/workflows/ci.yml`) automatically:
1. Builds the solution
2. Runs unit tests
3. Applies EF migrations against SQL Server service container

The workflow ensures code quality and database compatibility on every push.

## Development

### Build the Solution

```pwsh
dotnet build
```

### Run the API

```pwsh
cd src/MaziwaPlus.Api
dotnet run
```

Or with environment variable:

```pwsh
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
```

### VS Code Configuration

- **Debug**: Press `F5` or use `.vscode/launch.json` configurations
- **Build**: Press `Ctrl+Shift+B` or run build task
- **Test**: Use `dotnet test` command

## License

This project is part of the MaziwaPlus initiative for dairy supply chain management.

## Support

For issues or questions, please create an issue in the repository.

