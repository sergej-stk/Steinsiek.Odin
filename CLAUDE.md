# CLAUDE.md - Steinsiek.Odin

## Project Overview

Steinsiek.Odin is a modular .NET Aspire **Employee and Company Management** platform. The backend follows a **Modular Monolith** approach where each domain (Auth, Persons, Companies, etc.) is encapsulated in a separate Class Library. The frontend is a **Blazor Web App with Bootstrap 5** that communicates with the API via typed HttpClient services and Aspire service discovery.

## Technology Stack

- **.NET 10** with C# 13
- **.NET Aspire** for orchestration and service discovery
- **Entity Framework Core 10** with PostgreSQL (Npgsql) and InMemory provider
- **PostgreSQL** as primary database (via Aspire)
- **BCrypt.Net-Next** for password hashing
- **Bootstrap 5.3.8** for the Blazor Web App frontend (via CDN)
- **Bootstrap Icons 1.13** for iconography (via CDN)
- **Serilog** for structured logging
- **Scalar** for API documentation (instead of Swagger UI)
- **JWT Bearer** for authentication
- **Redis** for caching (via Aspire)
- **MSTest** for unit tests

## Solution Structure

```
Steinsiek.Odin/
├── Steinsiek.Odin.slnx                   # Main orchestration solution
├── src/
│   ├── Steinsiek.Odin.AppHost/           # Aspire Orchestration
│   ├── Steinsiek.Odin.ServiceDefaults/   # Shared Aspire Config
│   ├── Steinsiek.Odin.API/               # Host API (Program.cs, Middleware, Dashboard)
│   ├── Steinsiek.Odin.Web/              # Bootstrap 5 Frontend
│   └── Modules/
│       ├── Core/
│       │   ├── Core.slnx                        # Module-specific solution
│       │   ├── Steinsiek.Odin.Modules.Core/     # Shared Abstractions, Lookups, Audit
│       │   ├── Steinsiek.Odin.Modules.Core.Shared/
│       │   └── Steinsiek.Odin.Modules.Core.Tests/
│       ├── Auth/
│       │   ├── Auth.slnx                        # Module-specific solution
│       │   ├── Steinsiek.Odin.Modules.Auth/     # Auth Module (JWT + RBAC)
│       │   ├── Steinsiek.Odin.Modules.Auth.Shared/   # DTOs
│       │   └── Steinsiek.Odin.Modules.Auth.Tests/
│       ├── Persons/
│       │   ├── Persons.slnx                     # Module-specific solution
│       │   ├── Steinsiek.Odin.Modules.Persons/       # Persons Module
│       │   ├── Steinsiek.Odin.Modules.Persons.Shared/    # DTOs
│       │   └── Steinsiek.Odin.Modules.Persons.Tests/
│       └── Companies/
│           ├── Companies.slnx                   # Module-specific solution
│           ├── Steinsiek.Odin.Modules.Companies/      # Companies Module
│           ├── Steinsiek.Odin.Modules.Companies.Shared/   # DTOs
│           └── Steinsiek.Odin.Modules.Companies.Tests/
└── tests/
    └── Steinsiek.Odin.API.Tests/         # Integration tests
```

## Language Policy
- The entire project must be in English
- All code, comments, variable names, strings, and documentation must use English
- Demo data (person names, company names, lookup values) must be in English

## Authentication

- JWT token via `/api/v1/auth/login`
- Token in header: `Authorization: Bearer {token}`
- RBAC roles: Admin, Manager, User, ReadOnly (stored as JWT `ClaimTypes.Role` claims)
- Demo user: `demo@steinsiek.de` / `Demo123!` (Admin role)
- JWT config in `appsettings.json` under `Jwt`

## Development

### Build
```bash
cd Steinsiek.Odin
dotnet build
```

### Run (via Aspire)
```bash
dotnet run --project src/Steinsiek.Odin.AppHost
```

### Tests
```bash
dotnet test
```

### Frontend
After starting via Aspire, the Web frontend is available in the Aspire Dashboard under the `web` resource.

### API Documentation
After starting: `https://localhost:{port}/scalar/v1`
