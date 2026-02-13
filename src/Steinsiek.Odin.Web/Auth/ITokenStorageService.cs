namespace Steinsiek.Odin.Web.Auth;

/// <summary>
/// Provides secure storage for JWT authentication tokens.
/// </summary>
public interface ITokenStorageService
{
    /// <summary>
    /// Stores the JWT token securely.
    /// </summary>
    /// <param name="token">The JWT token to store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SetToken(string token, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the stored JWT token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The stored JWT token, or <c>null</c> if no token is stored.</returns>
    Task<string?> GetToken(CancellationToken cancellationToken);

    /// <summary>
    /// Removes the stored JWT token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task RemoveToken(CancellationToken cancellationToken);
}
