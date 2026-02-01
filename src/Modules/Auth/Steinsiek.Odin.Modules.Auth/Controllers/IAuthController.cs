namespace Steinsiek.Odin.Modules.Auth.Controllers;

/// <summary>
/// Defines the contract for authentication operations.
/// </summary>
public interface IAuthController
{
    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful.</returns>
    Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new user account and returns a JWT token.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful.</returns>
    Task<ActionResult<LoginResponse>> Register(RegisterRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns the current authenticated user's information.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The current user's details.</returns>
    Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken);
}
