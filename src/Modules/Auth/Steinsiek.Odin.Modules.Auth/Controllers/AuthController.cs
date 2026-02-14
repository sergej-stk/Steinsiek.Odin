namespace Steinsiek.Odin.Modules.Auth.Controllers;

/// <summary>
/// API controller for authentication and role management operations.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase, IAuthController
{
    private readonly IAuthService _authService = authService;

    /// <inheritdoc />
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.Login(request, cancellationToken);
        return result ?? (ActionResult<LoginResponse>)Unauthorized(new { message = "Invalid email or password" });
    }

    /// <inheritdoc />
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.Register(request, cancellationToken);
        return result is null ? (ActionResult<LoginResponse>)BadRequest(new { message = "Email already exists" }) : (ActionResult<LoginResponse>)CreatedAtAction(nameof(Login), result);
    }

    /// <inheritdoc />
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst(JwtClaimNames.Subject)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value
            ?? User.FindFirst(JwtClaimNames.Email)?.Value;
        var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value
            ?? User.FindFirst(JwtClaimNames.GivenName)?.Value ?? "";
        var lastName = User.FindFirst(ClaimTypes.Surname)?.Value
            ?? User.FindFirst(JwtClaimNames.FamilyName)?.Value ?? "";
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        return string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email)
            ? (ActionResult<UserDto>)Unauthorized()
            : (ActionResult<UserDto>)new UserDto
            {
                Id = Guid.Parse(userId),
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Roles = roles
            };
    }

    /// <inheritdoc />
    [HttpGet("roles")]
    [Authorize(Roles = OdinRoles.Admin)]
    [ProducesResponseType(typeof(IReadOnlyList<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetRoles(CancellationToken cancellationToken)
    {
        var roles = await _authService.GetAllRoles(cancellationToken);
        return Ok(roles);
    }

    /// <inheritdoc />
    [HttpPost("users/{userId:guid}/roles")]
    [Authorize(Roles = OdinRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AssignRole(Guid userId, [FromBody] AssignRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.AssignRole(userId, request.RoleId, cancellationToken);
        return result ? NoContent() : BadRequest(new { message = "Role already assigned" });
    }

    /// <inheritdoc />
    [HttpDelete("users/{userId:guid}/roles/{roleId:guid}")]
    [Authorize(Roles = OdinRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var result = await _authService.RemoveRole(userId, roleId, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}
