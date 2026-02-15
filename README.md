<div align="center">
  <a href="https://github.com/sergej-stk/Steinsiek.Odin">
    <img src="assets/logo.svg" alt="Steinsiek Logo" width="400">
  </a>

  <h3>Steinsiek</h3>

  <p>A modular .NET Aspire Employee and Company Management platform</p>

  [![Build](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/build.yml/badge.svg)](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/build.yml)
  [![Test](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/test.yml/badge.svg)](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/test.yml)
  [![Code Quality](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/codequality.yml/badge.svg)](https://github.com/sergej-stk/Steinsiek.Odin/actions/workflows/codequality.yml)

  [Documentation](https://github.com/sergej-stk/Steinsiek.Odin) · [Report Bug](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?template=bug-report.yml&labels=bug) · [Request Feature](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?template=feature-request.yml&labels=enhancement) · [Changelog](CHANGELOG.md)
</div>

---

## About

Steinsiek is a modular Employee and Company Management platform built with .NET 10 and ASP.NET Aspire. The project follows a **Modular Monolith** architecture where each domain (Auth, Persons, Companies) is encapsulated in a separate module, enabling clean separation of concerns while maintaining the simplicity of a monolithic deployment. The frontend is a Blazor Web App with Bootstrap 5.

## Features

- **JWT Authentication** - Secure token-based authentication
- **API Versioning** - URL-based versioning (`/api/v1/...`)
- **Redis Caching** - Distributed caching via Aspire
- **Scalar API Docs** - Modern OpenAPI documentation
- **Modular Architecture** - Clean domain separation (Auth, Persons, Companies, Core)
- **Aspire Orchestration** - Service discovery and health monitoring

## Preview

![Dashboard](assets/screenshots/dashboard.png)

<table>
<tr>
<td width="50%"><img src="assets/screenshots/landing-page.png" alt="Landing Page"></td>
<td width="50%"><img src="assets/screenshots/login.png" alt="Login"></td>
</tr>
<tr>
<td align="center"><sub>Landing Page</sub></td>
<td align="center"><sub>Login</sub></td>
</tr>
<tr>
<td width="50%"><img src="assets/screenshots/person-detail.png" alt="Person Detail"></td>
<td width="50%"><img src="assets/screenshots/company-list.png" alt="Company List"></td>
</tr>
<tr>
<td align="center"><sub>Person Detail</sub></td>
<td align="center"><sub>Company List</sub></td>
</tr>
</table>

## Tech Stack

| Category | Technology |
|----------|------------|
| Framework | .NET 10, C# 13 |
| Frontend | Blazor Server, Bootstrap 5 |
| Database | PostgreSQL, EF Core |
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
│   ├── Steinsiek.Odin.Web/              # Blazor Frontend
│   └── Modules/
│       ├── Core/                         # Shared Abstractions & Lookups
│       ├── Auth/                         # Authentication Module
│       ├── Persons/                      # Persons Module
│       └── Companies/                    # Companies Module
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

### Persons

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/v1/persons` | Yes | List all persons |
| GET | `/api/v1/persons/{id}` | Yes | Get person by ID |
| GET | `/api/v1/persons/search?q=` | Yes | Search persons |
| POST | `/api/v1/persons` | Yes | Create person |
| PUT | `/api/v1/persons/{id}` | Yes | Update person |
| DELETE | `/api/v1/persons/{id}` | Yes | Soft-delete person |

### Companies

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/v1/companies` | Yes | List all companies |
| GET | `/api/v1/companies/{id}` | Yes | Get company by ID |
| GET | `/api/v1/companies/search?q=` | Yes | Search companies |
| POST | `/api/v1/companies` | Yes | Create company |
| PUT | `/api/v1/companies/{id}` | Yes | Update company |
| DELETE | `/api/v1/companies/{id}` | Yes | Soft-delete company |

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

Contributions are welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md) before opening issues or submitting pull requests.

<a href="https://github.com/sergej-stk/Steinsiek.Odin/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=sergej-stk/Steinsiek.Odin" />
</a>

## Security

To report a security vulnerability, please use [GitHub's private vulnerability reporting](https://github.com/sergej-stk/Steinsiek.Odin/security/advisories/new). See [SECURITY.md](SECURITY.md) for details.

## License

This project is proprietary software. See [LICENSE.txt](LICENSE.txt) for details.
