namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client for authentication API operations.
/// </summary>
public interface IAuthApiClient
{
    /// <summary>
    /// Authenticates a user and returns a login response with JWT token.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response, or <c>null</c> if authentication failed.</returns>
    Task<LoginResponse?> Login(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response, or <c>null</c> if registration failed.</returns>
    Task<LoginResponse?> Register(RegisterRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the currently authenticated user's profile.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user profile, or <c>null</c> if not authenticated.</returns>
    Task<UserDto?> GetCurrentUser(CancellationToken cancellationToken);
}
