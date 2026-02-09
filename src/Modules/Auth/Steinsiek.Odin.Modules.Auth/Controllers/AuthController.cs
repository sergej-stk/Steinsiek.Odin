namespace Steinsiek.Odin.Modules.Auth.Controllers;

/// <summary>
/// API controller for authentication operations.
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
            ?? User.FindFirst("sub")?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value
            ?? User.FindFirst("email")?.Value;
        var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value
            ?? User.FindFirst("given_name")?.Value ?? "";
        var lastName = User.FindFirst(ClaimTypes.Surname)?.Value
            ?? User.FindFirst("family_name")?.Value ?? "";

        return string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email)
            ? (ActionResult<UserDto>)Unauthorized()
            : (ActionResult<UserDto>)new UserDto
            {
                Id = Guid.Parse(userId),
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
    }
}
