namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// Service interface for authentication operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful; otherwise, null.</returns>
    Task<LoginResponse?> Login(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful; otherwise, null.</returns>
    Task<LoginResponse?> Register(RegisterRequest request, CancellationToken cancellationToken);
}
