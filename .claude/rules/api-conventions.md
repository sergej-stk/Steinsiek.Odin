# API Conventions

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
| 403 Forbidden | Role-based authorization |

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
