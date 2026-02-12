# Contributing to Steinsiek.Odin

Thank you for your interest in contributing to Steinsiek.Odin! This guide will help you get started.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- IDE of choice (Visual Studio 2022+, JetBrains Rider, or VS Code)

### Setup

```bash
git clone https://github.com/sergej-stk/Steinsiek.Odin.git
cd Steinsiek.Odin
dotnet build
dotnet test
```

### Running the Application

```bash
dotnet run --project src/Steinsiek.Odin.AppHost
```

## Development Workflow

1. Create a new branch from `main`
2. Make your changes
3. Ensure all tests pass (`dotnet test`)
4. Ensure code formatting is correct (`dotnet format --verify-no-changes`)
5. Submit a pull request against `main`

## Code Style

This project enforces consistent code style via `.editorconfig` and CI checks. Key conventions:

- **Sealed classes** — All concrete implementations (controllers, services, repositories, DTOs) must be `sealed`
- **No `Async` suffix** — Use `GetById`, not `GetByIdAsync`. The `Task<T>` return type already indicates async behavior
- **Primary constructors** — Controllers must use primary constructors with explicit `private readonly` field assignments
- **XML documentation** — All public types and members require XML docs. Use `<inheritdoc />` for interface implementations
- **Global usings** — All `using` directives go in `GlobalUsings.cs` per project. No `using` statements in source files
- **CancellationToken** — Required as the last parameter in all async methods, with no default value

See [CLAUDE.md](CLAUDE.md) for the complete list of conventions.

## Module Architecture

The project follows a Modular Monolith pattern. Each module consists of three projects:

| Project | Contains |
|---------|----------|
| `Modules.{Name}` | Controllers, Services, Repositories, Entities |
| `Modules.{Name}.Shared` | DTOs, Contracts, Interfaces |
| `Modules.{Name}.Tests` | Unit and integration tests |

Cross-module dependencies are not allowed. See [CLAUDE.md](CLAUDE.md) for full architectural guidelines and instructions on adding a new module.

## Commit Conventions

- **Title**: Imperative mood, max 50 characters (e.g., `Add Auth module`)
- **Body**: Bullet points describing individual changes
- Start with an imperative verb: Add, Fix, Update, Remove, Refactor
- No Co-Authored-By lines
- English only

Example:

```
Add Auth module

- Add AuthModule with DI registration
- Add AuthController with login/register/me endpoints
- Add User entity
- Add repository and service layers
```

## Pull Requests

- Fill out the [pull request template](.github/PULL_REQUEST_TEMPLATE.md) completely
- All CI checks (Build, Test, Code Quality) must pass
- Keep changes focused — one feature or fix per PR

## Reporting Bugs

Use the [bug report template](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?template=bug-report.yml&labels=bug) to report issues.

## Requesting Features

Use the [feature request template](https://github.com/sergej-stk/Steinsiek.Odin/issues/new?template=feature-request.yml&labels=enhancement) to suggest new features.
