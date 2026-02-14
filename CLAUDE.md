# CLAUDE.md - Steinsiek.Odin

## Project Overview

Steinsiek.Odin is a modular .NET Aspire E-Commerce platform. The backend follows a **Modular Monolith** approach where each domain (Auth, Products, etc.) is encapsulated in a separate Class Library. The frontend is a **Blazor Web App with Bootstrap 5** that communicates with the API via typed HttpClient services and Aspire service discovery.

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
│   ├── Steinsiek.Odin.API/               # Host API (Program.cs, Middleware)
│   ├── Steinsiek.Odin.Web/              # Bootstrap 5 Frontend
│   └── Modules/
│       ├── Core/
│       │   ├── Core.slnx                        # Module-specific solution
│       │   ├── Steinsiek.Odin.Modules.Core/     # Shared Abstractions
│       │   ├── Steinsiek.Odin.Modules.Core.Shared/
│       │   └── Steinsiek.Odin.Modules.Core.Tests/
│       ├── Auth/
│       │   ├── Auth.slnx                        # Module-specific solution
│       │   ├── Steinsiek.Odin.Modules.Auth/     # Auth Module
│       │   ├── Steinsiek.Odin.Modules.Auth.Shared/   # DTOs
│       │   └── Steinsiek.Odin.Modules.Auth.Tests/
│       └── Products/
│           ├── Products.slnx                    # Module-specific solution
│           ├── Steinsiek.Odin.Modules.Products/      # Products Module
│           ├── Steinsiek.Odin.Modules.Products.Shared/   # DTOs
│           └── Steinsiek.Odin.Modules.Products.Tests/
└── tests/
    └── Steinsiek.Odin.API.Tests/         # Integration tests
```

## Module Architecture

Each module follows this internal structure:

```
Modules/{ModuleName}/
├── {ModuleName}.slnx                          # Module-specific solution
├── Steinsiek.Odin.Modules.{ModuleName}/       # Core Class Library
│   ├── {ModuleName}Module.cs                  # IModule Implementation (DI Registration)
│   ├── Controllers/                           # API Endpoints
│   ├── Entities/                              # Domain Entities (inherit from BaseEntity)
│   ├── Persistence/Configurations/            # IEntityTypeConfiguration<T> (Fluent API)
│   ├── Repositories/                          # I{Name}Repository + EF Implementation
│   └── Services/                              # I{Name}Service + Implementation
├── Steinsiek.Odin.Modules.{ModuleName}.Shared/    # Shared Contracts
│   └── DTOs/                                  # Request/Response Records
└── Steinsiek.Odin.Modules.{ModuleName}.Tests/     # Unit Tests
```

### Module Responsibility Rules

**Hard architectural constraints** that must be followed:

| Project | Contains | Does NOT Contain |
|---------|----------|------------------|
| `{ModuleName}` | Controllers, Services, Repositories, Entities, Entity Configurations, Application logic | DTOs, Shared contracts |
| `{ModuleName}.Shared` | DTOs, Contracts, Interfaces, Enums, Value objects, Constants | Controllers, Services, Business logic |
| `{ModuleName}.Tests` | Unit tests, Integration tests | Business logic, Production code |

**Failure Conditions:**
- Logic in `.Shared` is incorrect
- DTOs outside `.Shared` are incorrect
- Cross-module dependencies are incorrect
- Tests referencing other modules (except their own `.Shared`) is incorrect

### IModule Interface

Each module implements `IModule` from the Core module:

```csharp
public sealed class AuthModule : IModule
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
    }
}
```

Registration happens centrally in `ServiceCollectionExtensions.cs`:

```csharp
public static IServiceCollection AddModules(this IServiceCollection services)
{
    AuthModule.RegisterServices(services);
    ProductsModule.RegisterServices(services);
    return services;
}
```

## Language Policy
- The entire project must be in English
- All code, comments, variable names, strings, and documentation must use English
- Demo data (product names, descriptions, categories) must be in English

## Important Conventions

### Entities
- All entities inherit from `BaseEntity` (Id, CreatedAt, UpdatedAt)
- Use `required` modifier where appropriate
- Guid as primary key

### DTOs

**Mandatory immutability rule.** All DTOs must be immutable data contracts.

#### Implementation Rules
- Use `record class` (preferred) or `record` for all DTOs
- Use regular `class` only if technically necessary (must be justified)
- All properties must use `{ get; init; }` - no `set;` allowed
- DTOs are data contracts only - no behavior, business logic, or methods beyond constructors

#### Design Constraints
- Prefer property-based record class definitions
- Use positional records only when they clearly improve readability
- Prefer immutable collections (`IReadOnlyList<T>`, `ImmutableArray<T>`) over mutable ones
- Validation via Data Annotations
- Naming: `{Name}Dto`, `Create{Name}Request`, `Update{Name}Request`

#### Examples
```csharp
// Correct: Property-based sealed record class with init-only properties
public sealed record class ProductDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
}

// Correct: Positional sealed record (when it improves readability)
public sealed record CreateProductRequest(string Name, decimal Price);

// Incorrect: Mutable DTO
public class ProductDto { public string Name { get; set; } } // ❌
// Incorrect: Unsealed record
public record class ProductDto { ... } // ❌
```

#### Failure Conditions
- DTOs implemented as mutable classes are incorrect
- DTO properties using `set;` are incorrect
- Ignoring this rule without technical justification is incorrect

### Repositories
- Interface in `I{Name}Repository.cs`
- EF Core implementation using `OdinDbContext` via `context.Set<T>()`
- Scoped lifetime (matching DbContext lifetime)
- Demo data seeded via `HasData()` in entity configurations
- All async methods must accept `CancellationToken` as the last parameter (required, no default value)

### Services
- Interface + Implementation separated
- Scoped lifetime
- Logging via `ILogger<T>`
- No direct database access, only through repositories
- All async methods must accept `CancellationToken` as the last parameter (required, no default value)

### Controllers
- `[ApiController]` and `[Route("api/v{version:apiVersion}/[controller]")]`
- `[ApiVersion(1)]` on all controllers
- `[Authorize]` at controller level, `[AllowAnonymous]` for public endpoints
- `ProducesResponseType` attributes for OpenAPI

### Controller Attribute Ordering

**Mandatory ordering rule.** Controller attributes must appear in the following order:

1. `[ApiController]`
2. `[ApiVersion(1)]`
3. `[Route("api/v{version:apiVersion}/[controller]")]`
4. `[Authorize]` (if applicable)

#### Rules
- Apply this order to every controller without exception
- `[AllowAnonymous]` is only allowed on actions, never on controllers
- Do not reorder attributes based on personal preference
- Deviations are considered architectural violations

#### Example
```csharp
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class ProductsController : ControllerBase
```

#### Failure Conditions
- Controller attributes in wrong order are incorrect
- `[AllowAnonymous]` on a controller is incorrect
- Missing required attributes are incorrect

### Controller Interfaces

**Mandatory interface rule.** Every API controller must have a corresponding interface.

#### Rules
- Each controller `{Name}Controller` must implement an interface named `I{Name}Controller`
- The interface defines all public action methods of the controller
- The interface resides in the same module as the controller (in the `Controllers/` folder)
- The controller class must explicitly implement its interface
- Interfaces are not placed in `.Shared` projects

#### Example
```csharp
// Interface definition
public interface IProductsController
{
    Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken);
    Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken);
    Task<ActionResult<ProductDto>> Create(CreateProductRequest request, CancellationToken cancellationToken);
}

// Controller implementation
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class ProductsController : ControllerBase, IProductsController
{
    /// <inheritdoc />
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken) { ... }

    /// <inheritdoc />
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken) { ... }

    /// <inheritdoc />
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request, CancellationToken cancellationToken) { ... }
}
```

#### Failure Conditions
- Controllers without a corresponding interface are incorrect
- Interface not named `I{Name}Controller` is incorrect
- Interface placed in `.Shared` project is incorrect
- Controller not explicitly implementing its interface is incorrect

### Controller Primary Constructors

**Mandatory primary constructor rule.** Controllers must use C# primary constructors with explicit readonly field assignments.

#### Rules
- Controllers must use primary constructors whenever technically possible
- Injected dependencies must be assigned to `private readonly` fields
- Readonly fields must be explicitly declared in the controller body
- The primary constructor is used only for field assignment, not for logic
- Classic constructors are only allowed with explicit technical justification

#### Example
```csharp
// Correct: Primary constructor with explicit readonly field
public sealed class ProductsController(IProductService productService, ILogger<ProductsController> logger)
    : ControllerBase, IProductsController
{
    private readonly IProductService _productService = productService;
    private readonly ILogger<ProductsController> _logger = logger;
}

// Incorrect: Primary constructor without readonly fields
public sealed class ProductsController(IProductService productService)
    : ControllerBase, IProductsController
{
    // ❌ Missing explicit readonly field declaration
}

// Incorrect: Classic constructor when primary constructor is possible
public sealed class ProductsController : ControllerBase, IProductsController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) // ❌
    {
        _productService = productService;
    }
}
```

#### Failure Conditions
- Controllers using classic constructors without technical justification are incorrect
- Primary constructor parameters not assigned to readonly fields are incorrect
- Readonly fields not explicitly declared in the controller body are incorrect
- Logic in primary constructor assignments is incorrect

### Controller Action Return Types

**Mandatory return type rule.** Controller actions must use typed `ActionResult<T>` return types with required `CancellationToken`.

#### Return Type Rules
- Use `Task<ActionResult<T>>` for actions returning a single entity
- Use `Task<ActionResult<IEnumerable<T>>>` for actions returning collections
- Use `Task<IActionResult>` only for actions returning no content (e.g., Delete)
- All async actions must have the `async` keyword and use `await`

#### CancellationToken Rules
- `CancellationToken` must be the **last parameter** in all async methods
- `CancellationToken` is **required** (no default value allowed)
- Pass `CancellationToken` through all layers: Controller -> Service -> Repository

#### Implicit Conversion Rules
- Use implicit conversion where possible (return entity directly, not `Ok(entity)`)
- For success responses returning data: `return product;` instead of `return Ok(product);`
- For error responses: use explicit helper methods (`NotFound()`, `BadRequest()`, `Unauthorized()`)
- For `CreatedAtAction`: explicit call is required

#### Examples
```csharp
// Correct: Typed ActionResult with required CancellationToken
public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
{
    var product = await _productService.GetById(id, cancellationToken);
    if (product is null)
    {
        return NotFound();
    }
    return product; // Implicit conversion, no Ok() needed
}

// Correct: Collection return type
public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
{
    var products = await _productService.GetAll(cancellationToken);
    return Ok(products); // Ok() needed for IEnumerable to avoid ambiguity
}

// Correct: Delete action with IActionResult
public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
{
    var result = await _productService.Delete(id, cancellationToken);
    if (!result)
    {
        return NotFound();
    }
    return NoContent();
}

// Incorrect: IActionResult instead of typed ActionResult
public async Task<IActionResult> GetById(Guid id) { ... } // ❌

// Incorrect: CancellationToken with default value
public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken = default) { ... } // ❌

// Incorrect: Missing CancellationToken
public async Task<ActionResult<ProductDto>> GetById(Guid id) { ... } // ❌
```

#### Failure Conditions
- Using `IActionResult` instead of `ActionResult<T>` for data-returning actions is incorrect
- CancellationToken with default value is incorrect
- Missing CancellationToken parameter is incorrect
- CancellationToken not as last parameter is incorrect
- Using `Ok()` when implicit conversion is possible (single entity) is incorrect

### Async Method Naming

**Mandatory naming rule.** Async methods must NOT include the "Async" suffix in their names.

#### Rationale
- The return type `Task<T>` already indicates async behavior
- Modern C# conventions favor cleaner method names
- Reduces verbosity without losing clarity
- The `async` keyword and `await` usage make async nature explicit

#### Rules
- Method names must describe the action, not the implementation detail
- Use `GetById`, not `GetByIdAsync`
- Use `Create`, not `CreateAsync`
- Use `Delete`, not `DeleteAsync`
- This applies to all layers: Controllers, Services, Repositories
- Exception: Framework-required method signatures (e.g., middleware `InvokeAsync`) retain the Async suffix

#### Examples
```csharp
// Correct: No Async suffix
Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken);
Task<IEnumerable<ProductDto>> GetAll(CancellationToken cancellationToken);
Task<ProductDto> Create(CreateProductRequest request, CancellationToken cancellationToken);
Task<bool> Delete(Guid id, CancellationToken cancellationToken);

// Incorrect: Async suffix
Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken); // ❌
Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken); // ❌
Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken); // ❌
```

#### Failure Conditions
- Methods with "Async" suffix are incorrect
- Renaming existing methods to add "Async" suffix is incorrect

### ProducesResponseType with ActionResult<T>

When using `ActionResult<T>`, the `ProducesResponseType` attribute must still specify the type for OpenAPI documentation clarity.

#### Rules
- Always include `typeof(T)` in `ProducesResponseType` for success responses
- Status codes must be sorted in ascending order
- All possible response types must be documented

#### Example
```csharp
[HttpGet("{id:guid}")]
[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
```

### Sealed Classes

**Mandatory sealing rule.** All concrete implementation classes must be marked as `sealed` unless inheritance is explicitly required.

#### Classes That Must Be Sealed
- **Controllers**: All controller classes (inherit from `ControllerBase`)
- **Services**: All service implementations (implement `I{Name}Service`)
- **Repositories**: All repository implementations (implement `I{Name}Repository`)
- **DTOs**: All record classes in `.Shared` projects
- **Module classes**: All `{Name}Module` classes implementing `IModule`

#### Rationale
- Enables JIT devirtualization optimizations
- Communicates design intent (not designed for inheritance)
- Prevents accidental subclassing of DI-registered classes

#### Examples
```csharp
// Correct: Sealed controller
public sealed class ProductsController : ControllerBase

// Correct: Sealed service
public sealed class ProductService : IProductService

// Correct: Sealed record DTO
public sealed record class ProductDto

// Incorrect: Unsealed implementations
public class ProductService : IProductService // ❌
```

#### Failure Conditions
- Controllers without `sealed` are incorrect
- Service/Repository implementations without `sealed` are incorrect
- DTOs (record classes) without `sealed` are incorrect
- Module classes without `sealed` are incorrect

## API Versioning

The API uses URL-based versioning with the pattern `/api/v{version}/{controller}`.

### Version Format
- Major versions only: `v1`, `v2`, etc.
- Version is mandatory in all API requests
- No unversioned endpoints allowed

### Configuration
- All versioning configuration resides in `Steinsiek.Odin.API`
- Modules use `[ApiVersion]` attribute but remain version-agnostic
- Version routing handled by `Asp.Versioning.Mvc`

### Controller Version Attributes
```csharp
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class MyController : ControllerBase
```

### Adding a New API Version
1. Add `[ApiVersion(2)]` to controllers supporting v2
2. Use `[MapToApiVersion(2)]` on v2-specific actions
3. Register OpenAPI document: `builder.Services.AddOpenApi("v2", ...)`
4. Add to Scalar: `options.AddDocument("v2", ...)`

### Versioning Rules
- All existing endpoints are v1
- Default API version is v1 (explicit, not assumed)
- Version reported in `api-supported-versions` response header
- Deprecated versions use `[ApiVersion(1, Deprecated = true)]`

### Failure Conditions
- Unversioned controllers are incorrect
- Breaking changes without a new API version are incorrect
- Versioning logic inside modules is incorrect

## OpenAPI Security Documentation

### Security Scheme
JWT Bearer authentication is defined in the OpenAPI document via document transformers in `Program.cs`. The security scheme uses HTTP Bearer with JWT format.

### Configuration Location
- Security scheme definition: `Program.cs` in `AddOpenApi` configuration
- Operation transformer applies security requirements to protected endpoints automatically

### Controller-Level Security
- Use `[Authorize]` at controller level for controllers where most endpoints require authentication
- Use `[AllowAnonymous]` on individual actions that should be publicly accessible
- Controllers without `[Authorize]` are public by default

### ProducesResponseType Requirements
Protected endpoints must document 401 Unauthorized responses:

| Response Code | When Required |
|---------------|---------------|
| 401 Unauthorized | Endpoints with `[Authorize]` (no `[AllowAnonymous]`) |
| 403 Forbidden | Role-based authorization (future) |

### Attribute Ordering
`ProducesResponseType` attributes must be sorted by status code number (ascending):
- 200, 201, 204 (success codes first)
- 400, 401, 403 (client error codes)
- 404, 500 (remaining codes)

### Example
```csharp
[HttpPost]
[ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
```

### Failure Conditions
- Protected endpoints missing 401 response documentation are incorrect
- Security scheme not defined in OpenAPI is incorrect
- Endpoints without security requirements in OpenAPI spec are incorrect
- Implicit security behavior not documented is incorrect

## XML Documentation Comments

**Mandatory for all code output.** Every class, interface, method, property, and public function must have XML documentation.

### Rules
- **Language**: English only
- **Style**: Short, direct, precise
- **Focus**: Describe what the element represents or does, not implementation details
- **Avoid**: Redundancy, filler text, restating obvious code

### Formatting
- Follow standard C# XML documentation conventions
- Use all common and appropriate XML tags (`<summary>`, `<param>`, `<returns>`, `<remarks>`, `<exception>`, etc.)
- Keep documentation minimal but complete
- Do not document private implementation details unless explicitly requested

### Inheritance Rule
- If a member inherits from a base class or implements an interface that already has XML documentation -> use `<inheritdoc />`
- Do not duplicate or rewrite existing documentation in derived members unless explicitly required

### Failure Conditions
- Missing documentation is an incorrect response
- Ignoring `<inheritdoc />` where applicable is incorrect
- Non-English, verbose, or vague documentation is incorrect

## Global Using Directives

**All using directives must be centralized in GlobalUsings.cs files.**

### Rules
- Each project has exactly one `GlobalUsings.cs` file in the project root
- No source file may contain any `using` statements
- Implicit usings are disabled (`<ImplicitUsings>disable</ImplicitUsings>`)
- All required namespaces must be explicitly declared
- No comments allowed in GlobalUsings.cs files

### Example
```csharp
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.DependencyInjection;
global using Steinsiek.Odin.Modules.Core.Entities;
```

### Project-specific Guidelines
- **Shared projects**: `System`, `System.Collections.Generic`, `System.ComponentModel.DataAnnotations`
- **Test projects**: Include `Microsoft.VisualStudio.TestTools.UnitTesting`
- **Module projects**: Include ASP.NET Core, DI, `System.Threading`, and internal module namespaces

### Failure Conditions
- Using statements in source files (outside GlobalUsings.cs) are incorrect
- ImplicitUsings set to `enable` is incorrect
- Missing GlobalUsings.cs file in a project is incorrect
- Comments in GlobalUsings.cs files are incorrect

## Adding a New Module

1. Create module folder and projects:
```bash
cd src/Modules
mkdir {Name}
cd {Name}

# Create module projects
dotnet new classlib -n Steinsiek.Odin.Modules.{Name}
dotnet new classlib -n Steinsiek.Odin.Modules.{Name}.Shared
dotnet new mstest -n Steinsiek.Odin.Modules.{Name}.Tests

# Create module solution
dotnet new sln -n {Name}
dotnet sln {Name}.slnx add Steinsiek.Odin.Modules.{Name}/Steinsiek.Odin.Modules.{Name}.csproj
dotnet sln {Name}.slnx add Steinsiek.Odin.Modules.{Name}.Shared/Steinsiek.Odin.Modules.{Name}.Shared.csproj
dotnet sln {Name}.slnx add Steinsiek.Odin.Modules.{Name}.Tests/Steinsiek.Odin.Modules.{Name}.Tests.csproj
```

2. Add to main solution:
```bash
cd C:\Development\Steinsiek.Odin
dotnet sln add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}/Steinsiek.Odin.Modules.{Name}.csproj
dotnet sln add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Shared/Steinsiek.Odin.Modules.{Name}.Shared.csproj
dotnet sln add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Tests/Steinsiek.Odin.Modules.{Name}.Tests.csproj
```

3. Add project references:
```bash
# Module references Core and its own Shared
dotnet add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name} reference src/Modules/Core/Steinsiek.Odin.Modules.Core
dotnet add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name} reference src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Shared

# API references both Module and Shared
dotnet add src/Steinsiek.Odin.API reference src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}
dotnet add src/Steinsiek.Odin.API reference src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Shared

# Tests reference Module and Shared
dotnet add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Tests reference src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}
dotnet add src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Tests reference src/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Shared
```

4. Update module `.csproj` with ASP.NET Core framework reference:
```xml
<ItemGroup>
  <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>
```

5. Update `.Shared.csproj` with annotations package:
```xml
<ItemGroup>
  <PackageReference Include="System.ComponentModel.Annotations" Version="5.0.0" />
</ItemGroup>
```

6. Create `GlobalUsings.cs` in each project with required namespaces

7. Create `{Name}Module.cs` implementing `IModule`

8. Create DTOs in `.Shared/DTOs/` folder

9. Register in `ServiceCollectionExtensions.cs`

10. Add assembly in `Program.cs`:
```csharp
.AddApplicationPart(typeof(Steinsiek.Odin.Modules.{Name}.{Name}Module).Assembly)
```

11. Create entity type configurations in `Persistence/Configurations/` folder

12. Register module assembly in `ServiceCollectionExtensions.AddDatabase()`:
```csharp
options.ModuleAssemblies.Add(typeof(Steinsiek.Odin.Modules.{Name}.{Name}Module).Assembly);
```

13. Generate migration:
```bash
dotnet ef migrations add Add{Name}Module --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API --output-dir Migrations
```

## Database Configuration

### Provider Switch
The application supports two database providers, configured via `DatabaseProvider` in `appsettings.json`:

| Provider | Value | Use Case |
|----------|-------|----------|
| PostgreSQL | `"PostgreSQL"` | Production, Aspire orchestration |
| EF InMemory | `"InMemory"` | Development without DB, integration tests |

When running via Aspire AppHost, `DatabaseProvider` is set to `"PostgreSQL"` via environment variable.

### Connection Strings
- PostgreSQL connection string is provided by Aspire via `ConnectionStrings:odindb`
- InMemory mode requires no connection string

### Database Schemas
Each module owns a dedicated schema:

| Module | Schema | Tables |
|--------|--------|--------|
| Auth | `auth` | `Users` |
| Products | `products` | `Products`, `Categories`, `ProductImages` |

### Auto-Migration
- **PostgreSQL**: `Database.Migrate()` runs at startup, applying pending migrations
- **InMemory**: `Database.EnsureCreated()` creates the schema from the model

## Entity Framework Core

### OdinDbContext
- Central `DbContext` in `Steinsiek.Odin.Modules.Core.Persistence`
- No direct `DbSet<T>` properties - modules use `context.Set<T>()`
- Entity configurations are discovered via `ApplyConfigurationsFromAssembly()` from registered module assemblies
- Module assemblies registered via `OdinDbContextOptions`

### Entity Type Configurations
Each entity has an `IEntityTypeConfiguration<T>` in its module's `Persistence/Configurations/` folder:
- Table name and schema assignment
- Property constraints (MaxLength, Precision, Required)
- Relationships and foreign keys
- Unique indexes
- Seed data via `HasData()`

### Migrations
- Stored in `src/Modules/Core/Steinsiek.Odin.Modules.Core/Migrations/`
- Generated via `dotnet ef` CLI with the API as startup project
- `DesignTimeDbContextFactory` in the API project provides migration tooling support

### Migration Commands
```bash
# Add a new migration
dotnet ef migrations add {MigrationName} --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API --output-dir Migrations

# Remove last migration (if not applied)
dotnet ef migrations remove --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API

# List migrations
dotnet ef migrations list --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API
```

### Repository Pattern with EF Core
- Repositories use `OdinDbContext` injected via primary constructor
- Access entities via `context.Set<T>()` (no typed DbSet properties)
- All repositories are **Scoped** (not Singleton)
- `SaveChangesAsync()` called within each repository method

## Password Hashing

### IPasswordHasher Interface
- `Hash(string password)` - creates a BCrypt hash
- `Verify(string password, string hash)` - verifies password against hash

### Implementation
- `BcryptPasswordHasher` uses BCrypt.Net-Next (registered as Singleton)
- Password hashing happens in the `AuthService`, not in the repository
- Seed data uses a pre-computed BCrypt hash constant for migration stability

### Migration from SHA256
The project previously used SHA256 hashing. All password hashes are now BCrypt format (`$2a$11$...`).

## Frontend Architecture

### Overview
The frontend is a **Blazor Web App** (`Steinsiek.Odin.Web`) with **Interactive Server** rendering mode. It uses **Bootstrap 5.3** for UI styling.

### Project Structure
```
src/Steinsiek.Odin.Web/
├── Program.cs                          # Entry point, DI, Serilog, Bootstrap
├── GlobalUsings.cs                     # Global using directives
├── Auth/                               # JWT auth state management
│   ├── ITokenStorageService.cs
│   ├── TokenStorageService.cs          # ProtectedSessionStorage-based
│   └── JwtAuthenticationStateProvider.cs
├── Services/                           # Typed HttpClient API clients + Toast
│   ├── ApiAuthenticationHandler.cs     # DelegatingHandler for JWT
│   ├── IAuthApiClient.cs + AuthApiClient.cs
│   ├── IProductApiClient.cs + ProductApiClient.cs
│   ├── ICategoryApiClient.cs + CategoryApiClient.cs
│   ├── IToastService.cs + ToastService.cs  # Toast notification service
│   ├── ToastLevel.cs                   # Toast severity enum
│   └── ToastMessage.cs                 # Toast message model
├── Components/
│   ├── App.razor                       # Root HTML component
│   ├── Routes.razor                    # Router with AuthorizeRouteView
│   ├── _Imports.razor                  # Global Razor imports
│   ├── Layout/                         # MainLayout, NavMenu
│   ├── Pages/                          # Route pages
│   ├── Products/                       # Product components
│   ├── Auth/                           # Auth form components
│   └── Shared/                         # Reusable components (ToastContainer, ConfirmDialog, etc.)
└── wwwroot/                            # Static assets (CSS, JS interop)
```

### API Communication
- Typed `HttpClient` services with Aspire service discovery (`https+http://api`)
- References `.Shared` projects for DTO types (no duplication)
- `ApiAuthenticationHandler` attaches JWT Bearer token to all outgoing requests
- The Web project does NOT reference module core projects (only `.Shared`)

### Authentication Flow
1. User submits credentials via `LoginForm`
2. `AuthApiClient` calls `POST /api/v1/auth/login`
3. JWT token stored in `ProtectedSessionStorage` (server-side encrypted)
4. `JwtAuthenticationStateProvider` parses JWT claims for `AuthenticationState`
5. `<AuthorizeView>` and `@attribute [Authorize]` control UI visibility

### Frontend Conventions
- Blazor components use `_Imports.razor` for Razor-scoped imports
- C# code-behind files use `GlobalUsings.cs` (same as backend)
- API client services follow interface-first pattern (`I{Name}ApiClient`)
- All service implementations are `sealed`
- Pages use `@attribute [Authorize]` for protected routes
- No business logic in components — all API interaction through services
- Custom `IToastService` for user notifications (`ToastService.Show(message, ToastLevel)`)
- Bootstrap Modals for confirmation and form dialogs (declarative inline components with `Visible` parameter)
- Forms use `EditForm` + `DataAnnotationsValidator` for form validation
- Icons use Bootstrap Icons (`bi-*` classes)

### Theme
- CSS Custom Properties for dark/light mode (`data-theme` attribute toggle)
- Bootstrap standard color scheme (Blue/Grey), Inter font
- Dark mode toggle in navbar

## Authentication

- JWT token via `/api/v1/auth/login`
- Token in header: `Authorization: Bearer {token}`
- Demo user: `demo@steinsiek.de` / `Demo123!`
- JWT config in `appsettings.json` under `Jwt`

## API Endpoints

| Endpoint | Auth | Description |
|----------|------|-------------|
| POST /api/v1/auth/login | No | Login |
| POST /api/v1/auth/register | No | Registration |
| GET /api/v1/auth/me | Yes | Current user |
| GET /api/v1/products | No | All products |
| GET /api/v1/products/{id} | No | Product details |
| GET /api/v1/products/search?q= | No | Search |
| POST /api/v1/products | Yes | Create product |
| PUT /api/v1/products/{id} | Yes | Update product |
| DELETE /api/v1/products/{id} | Yes | Delete product |
| GET /api/v1/products/{id}/image | No | Product image |
| PUT /api/v1/products/{id}/image | Yes | Upload product image |
| DELETE /api/v1/products/{id}/image | Yes | Delete product image |
| GET /api/v1/categories | No | All categories |
| POST /api/v1/categories | Yes | Create category |

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

## Configuration

### appsettings.json Structure
```json
{
  "Serilog": { ... },
  "Jwt": {
    "Key": "...",
    "Issuer": "Steinsiek.Odin",
    "Audience": "Steinsiek.Odin.API",
    "ExpirationHours": 24
  },
  "DatabaseProvider": "InMemory"
}
```

- `DatabaseProvider`: `"InMemory"` (default) or `"PostgreSQL"` (set by Aspire)
- `ConnectionStrings:odindb`: Provided automatically by Aspire when using PostgreSQL

### Aspire AppHost
Redis, PostgreSQL, API, and Web frontend are orchestrated in `AppHost.cs`:
```csharp
var redis = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();
var odinDb = postgres.AddDatabase("odindb");

var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
    .WithReference(redis)
    .WithReference(odinDb)
    .WaitFor(redis)
    .WaitFor(postgres)
    .WithEnvironment("DatabaseProvider", "PostgreSQL");

builder.AddProject<Projects.Steinsiek_Odin_Web>("web")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();
```

## Known IDs (Demo Data)

```csharp
// User
Guid.Parse("11111111-1111-1111-1111-111111111111") // demo@steinsiek.de

// Categories
Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") // Electronics
Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") // Clothing
Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc") // Books
Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd") // Household

// Products
Guid.Parse("11111111-0001-0001-0001-000000000001") // iPhone 15 Pro
Guid.Parse("11111111-0001-0001-0001-000000000002") // MacBook Air M3
Guid.Parse("11111111-0001-0001-0001-000000000003") // Samsung Galaxy S24 Ultra
Guid.Parse("11111111-0001-0001-0001-000000000004") // Premium Hoodie
Guid.Parse("11111111-0001-0001-0001-000000000005") // Designer Jeans
Guid.Parse("11111111-0001-0001-0001-000000000006") // Clean Code
Guid.Parse("11111111-0001-0001-0001-000000000007") // Design Patterns
Guid.Parse("11111111-0001-0001-0001-000000000008") // Coffee Machine Deluxe
```

## Git Commit Conventions

### Commit Message Format
- **Title**: Short, imperative mood, max 50 characters
- **Body**: Bullet points describing changes
- **No AI attribution**: Omit Co-Authored-By lines

### Rules
- Use English for all commit messages
- Start with imperative verb (Add, Fix, Update, Remove, Refactor)
- No filler words or unnecessary descriptions
- Each bullet point describes one logical change
- No periods at end of title or bullet points

### Example
```
Add Auth module

- Add AuthModule with DI registration
- Add AuthController with login/register/me endpoints
- Add User entity
- Add repository and service layers
- Add DTOs for authentication
```

### Commit Grouping
- Group related changes logically
- One commit per module/feature when adding new code
- Separate commits for unrelated changes
- Configuration changes can be bundled if related

### Failure Conditions
- Commit messages not in English are incorrect
- Non-imperative title is incorrect
- Co-Authored-By lines are incorrect
- Filler words in commit messages are incorrect

## GitHub CLI Workflow

All GitHub operations use the **GitHub CLI** (`gh`).

### Installation
GitHub CLI is installed via `winget` and located at `C:\Program Files\GitHub CLI\gh.exe`.

### Branch + PR Workflow
1. Create a feature branch from `main`:
```bash
git checkout -b feature/{feature-name}
```

2. Commit changes and push with upstream tracking:
```bash
git push -u origin feature/{feature-name}
```

3. Create a Pull Request via `gh`:
```bash
gh pr create --title "Add feature X" --body "## Summary
- Change 1
- Change 2

## Test plan
- [ ] Verification step 1
- [ ] Verification step 2"
```

4. Merge the PR:
```bash
gh pr merge {pr-number} --merge
```

5. Return to `main` and pull:
```bash
git checkout main && git pull origin main
```

### Branch Naming
- `feature/{name}` for new features
- `fix/{name}` for bug fixes
- `refactor/{name}` for refactoring
- Use kebab-case for branch names

### PR Conventions
- Title: Short, imperative mood (same as commit title rules)
- Body: `## Summary` with bullet points, `## Test plan` with checklist
- Target branch: `main`
- Merge strategy: merge commit (not squash or rebase)

### Failure Conditions
- PRs without a summary section are incorrect
- Direct pushes to `main` for feature work are incorrect
- PRs without a test plan are incorrect
