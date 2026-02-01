<div align="center">

<!-- Logo placeholder: Add your logo here -->
<!-- <img src="assets/logo.png" alt="Steinsiek.Odin Logo" width="200" height="200"> -->

### Steinsiek.Odin

A modular .NET Aspire E-Commerce REST API

[Documentation](https://github.com/sergej-stk/Steinsiek.Odin) · [Report Bug](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?labels=bug) · [Request Feature](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?labels=enhancement)

</div>

---

## About

Steinsiek.Odin is a modular e-commerce REST API built with .NET 10 and ASP.NET Aspire. The project follows a **Modular Monolith** architecture where each domain (Auth, Products, Categories) is encapsulated in a separate module, enabling clean separation of concerns while maintaining the simplicity of a monolithic deployment.

## Features

- **JWT Authentication** - Secure token-based authentication
- **API Versioning** - URL-based versioning (`/api/v1/...`)
- **Redis Caching** - Distributed caching via Aspire
- **Scalar API Docs** - Modern OpenAPI documentation
- **Modular Architecture** - Clean domain separation (Auth, Products, Core)
- **Aspire Orchestration** - Service discovery and health monitoring

## Tech Stack

| Category | Technology |
|----------|------------|
| Framework | .NET 10, C# 13 |
| Orchestration | ASP.NET Aspire |
| Logging | Serilog |
| API Docs | Scalar |
| Authentication | JWT Bearer |
| Caching | Redis |
| Testing | MSTest |

## Project Structure

```
Steinsiek.Odin/
├── src/
│   ├── Steinsiek.Odin.AppHost/           # Aspire Orchestration
│   ├── Steinsiek.Odin.ServiceDefaults/   # Shared Aspire Config
│   ├── Steinsiek.Odin.API/               # Host API
│   └── Modules/
│       ├── Core/                         # Shared Abstractions
│       ├── Auth/                         # Authentication Module
│       └── Products/                     # Products & Categories Module
└── tests/
    └── Steinsiek.Odin.API.Tests/         # Integration Tests
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- IDE of choice (Visual Studio 2022+, JetBrains Rider, or VS Code)

## Installation

Clone the repository and build the solution:

```bash
git clone https://github.com/sergej-stk/Steinsiek.Odin.git
cd Steinsiek.Odin
dotnet build
```

## Running the Application

Start the application via Aspire orchestration:

```bash
dotnet run --project src/Steinsiek.Odin.AppHost
```

After startup:
- **Aspire Dashboard** - Available at the URL shown in console
- **Scalar API Docs** - `https://localhost:{port}/scalar/v1`

## API Endpoints

### Authentication

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/v1/auth/login` | No | User login |
| POST | `/api/v1/auth/register` | No | User registration |
| GET | `/api/v1/auth/me` | Yes | Current user info |

### Products

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/v1/products` | No | List all products |
| GET | `/api/v1/products/{id}` | No | Get product by ID |
| GET | `/api/v1/products/search?q=` | No | Search products |
| POST | `/api/v1/products` | Yes | Create product |
| PUT | `/api/v1/products/{id}` | Yes | Update product |
| DELETE | `/api/v1/products/{id}` | Yes | Delete product |

### Categories

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/v1/categories` | No | List all categories |
| GET | `/api/v1/categories/{id}` | No | Get category by ID |
| POST | `/api/v1/categories` | Yes | Create category |
| PUT | `/api/v1/categories/{id}` | Yes | Update category |
| DELETE | `/api/v1/categories/{id}` | Yes | Delete category |

### Demo Credentials

```
Email:    demo@steinsiek.de
Password: Demo123!
```

## Testing

Run all tests:

```bash
dotnet test
```

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

<a href="https://github.com/sergej-stk/Steinsiek.Odin/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=sergej-stk/Steinsiek.Odin" />
</a>

## License

This project is proprietary software. See [LICENSE.txt](LICENSE.txt) for details.
