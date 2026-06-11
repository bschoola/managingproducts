# Products Management API

A RESTful API built with ASP.NET Core 10.0 demonstrating CRUD operations for product management with an in-memory database, following Clean Architecture principles.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Validations](#validations)
- [Error Handling](#error-handling)
- [Tests](#tests)
- [Next Steps](#next-steps)

## Overview

A study project for a RESTful API built with ASP.NET Core 10.0 for product management. It uses an in-memory database, making it ideal for development, learning, and running tests without external dependencies.

## Architecture

The solution follows Clean Architecture with a clear separation of concerns across layers:

- **Api.Products** — Presentation layer: controllers, filters, and pipeline configuration
- **Domain.Products** — Domain layer: services, DTOs, interfaces, and validators
- **Infrastructure.Products** — Infrastructure layer: EF Core context, entities, and configurations
- **Api.Products.Tests** — Unit tests for the API layer

## Technologies

| Category | Technology |
|----------|------------|
| Framework | .NET 10.0 / ASP.NET Core Web API |
| ORM | Entity Framework Core 10.0 (In-Memory) |
| Validation | FluentValidation 12.1 |
| Documentation | Swagger / Swashbuckle 6.6 |
| Tests | xUnit 2.9, Moq 4.20, FluentAssertions 8.8 |
| Coverage | Coverlet |

## Project Structure

```
managingproducts/
├── Api.Products/                        # Presentation Layer
│   ├── Controllers/
│   │   └── ProductsController.cs        # Product CRUD endpoints
│   ├── Global/
│   │   └── GlobalExceptionFilter.cs     # Global exception filter with logging
│   ├── Program.cs                       # Entry point and DI configuration
│   └── appsettings.json
│
├── Domain.Products/                     # Domain Layer
│   ├── Contracts/
│   │   └── IProductService.cs           # Service interface
│   ├── Dto/
│   │   └── ProductDto.cs                # Data Transfer Object
│   ├── Services/
│   │   └── ProductService.cs            # Business logic
│   └── Validators/
│       └── ProductValidator.cs          # Validation rules (FluentValidation)
│
├── Infrastructure.Products/             # Infrastructure Layer
│   ├── Context/
│   │   └── ProductsContext.cs           # EF Core DbContext
│   ├── Entities/
│   │   └── Product.cs                   # Product entity
│   └── Configurations/
│       └── ProductConfiguration.cs      # Entity type configuration
│
└── Tests/
    └── Api.Products.Tests/              # Unit Tests
        └── Controllers/
            └── ProductsControllerTests.cs
```

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

### Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd managingproducts
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the API**
   ```bash
   cd Api.Products
   dotnet run
   ```

4. **Access the Swagger UI**

   Open your browser and navigate to `http://localhost:5230/swagger`

## API Endpoints

| Method | Route | Description | Success Status |
|--------|-------|-------------|----------------|
| GET | `/api/products` | List all products | `200 OK` |
| GET | `/api/products/{id}` | Get product by ID | `200 OK` |
| POST | `/api/products` | Create a new product | `200 OK` |
| PUT | `/api/products/{id}` | Update an existing product | `200 OK` |
| DELETE | `/api/products/{id}` | Remove a product | `204 No Content` |

### Request/Response Examples

#### POST /api/products

**Body:**
```json
{
  "name": "Laptop",
  "description": "High-performance notebook",
  "price": 999.99
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance notebook",
  "price": 999.99,
  "createdDate": "2026-05-16T10:30:00Z"
}
```

#### PUT /api/products/{id}

**Body:**
```json
{
  "name": "Laptop Pro",
  "description": "Professional notebook",
  "price": 1299.99
}
```

#### Error Response (400 Bad Request)

```json
{
  "error": "Product does not exist"
}
```

## Validations

Validations are implemented with FluentValidation and applied automatically via middleware:

| Field | Rules |
|-------|-------|
| `name` | Required, not empty, max 100 characters |
| `description` | Optional, max 150 characters |
| `price` | Greater than or equal to 0 |

## Error Handling

The `GlobalExceptionFilter` intercepts all unhandled exceptions and returns structured responses with logging:

| Exception | HTTP Status | Log Level |
|-----------|-------------|-----------|
| `InvalidOperationException` | `400 Bad Request` | Warning |
| Any other | `500 Internal Server Error` | Error |

All error responses follow the format:
```json
{ "error": "descriptive message" }
```

## Tests

The project includes controller unit tests using xUnit, Moq, and FluentAssertions.

### Run the tests

```bash
cd Tests/Api.Products.Tests
dotnet test
```

### Covered cases

| Test | Status |
|------|--------|
| List all products | OK |
| Get product by valid ID | OK |
| Create product with valid data | OK |
| Create product with invalid model state | OK |
| Update product with valid data | OK |
| Update product with invalid model state | OK |
| Delete product by ID | OK |
| Exception when deleting non-existent product | OK |

## Next Steps

Suggested improvements to evolve the project toward a production scenario:

**Architecture**
- [ ] Abstract DI registration into extension methods per layer (`AddInfrastructure()`, `AddDomain()`)
- [ ] Implement explicit Repository pattern to separate data access from the service
- [ ] Add AutoMapper for entity-to-DTO mapping

**Data**
- [ ] Migrate to a persistent database (SQL Server, PostgreSQL) with EF Core migrations
- [ ] Configure decimal precision for the `Price` field (`decimal(18,2)`)

**Security & Quality**
- [ ] Implement authentication and authorization (JWT)
- [ ] Configure CORS
- [ ] Add rate limiting

**Tests**
- [ ] Add unit tests for `ProductService` and `ProductValidator`
- [ ] Add integration tests with a real in-memory database

**Observability**
- [ ] Configure structured logging (Serilog or OpenTelemetry)
- [ ] Add health checks (`/health`)

---

> Educational project demonstrating Clean Architecture, validation with FluentValidation, and mock-based testing in ASP.NET Core 10.0.
