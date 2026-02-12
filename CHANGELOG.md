# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/), and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

## [0.1.0] - 2026-02-13

### Added

- Modular monolith project structure with Core, Auth, and Products modules
- JWT Bearer authentication with login, register, and current user endpoints
- Products module with CRUD operations and search
- Categories management with CRUD operations
- API versioning with URL-based routing (`/api/v1/...`)
- Redis caching via .NET Aspire orchestration
- Serilog structured logging with LogContext enrichment middleware
- Scalar API documentation
- Custom exception hierarchy (`OdinException`, `ElementNotFoundException`, `OdinValidationException`, `BusinessRuleException`)
- `ListResult<T>` wrapper for collection endpoints with `TotalCount` and `Data`
- Unit and integration test suites with MSTest
- CI/CD pipeline with build, test, and code quality workflows
- GitHub issue templates for bug reports and feature requests
- Pull request template with checklist
- Dependabot configuration for NuGet and GitHub Actions
- Project logo and branding

### Changed

- Centralized build configuration via `Directory.Build.props`
- Removed `Async` suffix from all method names
- Enforced CRLF line endings via `.gitattributes`

### Fixed

- MSTestSettings.cs file encoding
- CI pipeline restore failures
- README issue template links

[Unreleased]: https://github.com/sergej-stk/Steinsiek.Odin/compare/v0.1.0...HEAD
[0.1.0]: https://github.com/sergej-stk/Steinsiek.Odin/releases/tag/v0.1.0
