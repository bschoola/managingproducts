# Managing Products API

A simple ASP.NET Core Web API project demonstrating CRUD (Create, Read, Update, Delete) operations for product management using an in-memory database and following clean architecture principles.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Features](#features)

## 🎯 Overview

This project is a demonstration of a RESTful API built with ASP.NET Core 8.0 that manages products. It uses an in-memory database for data storage, making it ideal for development, testing, and learning purposes.

## 🏗️ Architecture

The solution follows a clean architecture approach with separation of concerns across multiple layers:

- **Api.Products** - Presentation layer containing controllers and API configuration
- **Domain.Products** - Business logic layer with services, DTOs, and validators
- **Infrastructure.Products** - Data access layer with Entity Framework Core configuration
- **Api.Products.Tests** - Unit tests for the API layer

## 🛠️ Technologies

- **.NET 8.0** - Target framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core** - ORM with In-Memory Database provider
- **FluentValidation** - Model validation
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework
- **Moq** - Mocking framework for unit tests
- **FluentAssertions** - Assertion library for tests

## 📁 Project Structure

```
managingproducts/
├── Api.Products/                    # Web API Project
│   ├── Controllers/
│   │   └── ProductsController.cs    # Products CRUD endpoints
│   ├── Global/
│   │   └── GlobalExceptionFilter.cs # Global exception handling
│   ├── Program.cs                   # Application entry point
│   └── appsettings.json            # Configuration
│
├── Domain.Products/                 # Business Logic Layer
│   ├── Contracts/
│   │   └── IProductService.cs      # Service interface
│   ├── Dto/
│   │   └── ProductDto.cs           # Data Transfer Object
│   ├── Services/
│   │   └── ProductService.cs       # Business logic implementation
│   └── Validators/
│       └── ProductValidator.cs     # FluentValidation rules
│
├── Infrastructure.Products/         # Data Access Layer
│   ├── Context/
│   │   └── ProductsContext.cs      # EF Core DbContext
│   ├── Entities/
│   │   └── Product.cs              # Product entity model
│   └── Configurations/
│       └── ProductConfiguration.cs  # EF Core entity configuration
│
└── Tests/
    └── Api.Products.Tests/          # Unit Tests
        └── Controllers/
            └── ProductsControllerTests.cs
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

### Running the Application

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

4. **Access Swagger UI**
   
   Open your browser and navigate to:
   - `https://localhost:<port>/swagger` (HTTPS)
   - `http://localhost:<port>/swagger` (HTTP)
   
   The port number will be displayed in the console output.

## 📡 API Endpoints

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get a product by ID |
| POST | `/api/products` | Create a new product |
| PUT | `/api/products/{id}` | Update an existing product |
| DELETE | `/api/products/{id}` | Delete a product |

### Request/Response Examples

#### Create Product (POST)

**Request:**
```json
{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "createdDate": "2025-11-11T10:30:00Z"
}
```

#### Update Product (PUT)

**Request:**
```json
{
  "name": "Laptop Pro",
  "description": "Professional grade laptop",
  "price": 1299.99
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Laptop Pro",
  "description": "Professional grade laptop",
  "price": 1299.99,
  "createdDate": "2025-11-11T10:30:00Z"
}
```

## 🧪 Testing

The project includes comprehensive unit tests for the Products Controller using xUnit, Moq, and FluentAssertions.

### Running Tests

```bash
cd Tests/Api.Products.Tests
dotnet test
```

### Test Coverage

The test suite covers:
- ✅ Getting all products
- ✅ Getting a product by ID
- ✅ Creating a new product with valid data
- ✅ Creating a product with invalid model state
- ✅ Updating an existing product
- ✅ Updating with invalid model state
- ✅ Deleting a product
- ✅ Exception handling when product doesn't exist

## ✨ Features

### Validation

Products are validated using FluentValidation with the following rules:
- **Name**: Required, maximum length of 100 characters
- **Price**: Must be greater than or equal to 0

### Exception Handling

Global exception handling is implemented via `GlobalExceptionFilter` to provide consistent error responses.

### In-Memory Database

The application uses Entity Framework Core's In-Memory Database provider, which means:
- No external database setup required
- Data persists only during application runtime
- Perfect for development and testing
- Data is reset when the application restarts

### API Documentation

Swagger/OpenAPI integration provides:
- Interactive API documentation
- API endpoint testing interface
- Automatic request/response schema generation

## 🔧 Configuration

The in-memory database is configured in `Program.cs`:

```csharp
builder.Services.AddDbContext<ProductsContext>(
    opt => opt.UseInMemoryDatabase("Test")
);
```

To switch to a real database (SQL Server, PostgreSQL, etc.), replace the `UseInMemoryDatabase` call with the appropriate database provider.

## 📝 Notes

- This is a learning/demonstration project designed to showcase clean architecture and testing practices
- The in-memory database means all data is lost when the application stops
- For production use, consider implementing:
  - A persistent database
  - Authentication and authorization
  - Logging and monitoring
  - Rate limiting
  - Caching strategies
  - AutoMapper for DTO mappings

## 📄 License

This project is for educational purposes.
