# Code Conventions

## Important Conventions

### Entities
- All entities inherit from `BaseEntity` (Id, CreatedAt, UpdatedAt, IsDeleted, DeletedAt)
- Soft-delete is handled transparently via global query filter and `SaveChangesAsync` override
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
