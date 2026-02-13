namespace Steinsiek.Odin.Web.Auth;

/// <summary>
/// Stores JWT tokens securely using Blazor's protected session storage.
/// </summary>
public sealed class TokenStorageService(ProtectedSessionStorage sessionStorage) : ITokenStorageService
{
    private const string TokenKey = "auth_token";

    private readonly ProtectedSessionStorage _sessionStorage = sessionStorage;

    /// <inheritdoc />
    public async Task SetToken(string token, CancellationToken cancellationToken)
    {
        await _sessionStorage.SetAsync(TokenKey, token);
    }

    /// <inheritdoc />
    public async Task<string?> GetToken(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sessionStorage.GetAsync<string>(TokenKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task RemoveToken(CancellationToken cancellationToken)
    {
        await _sessionStorage.DeleteAsync(TokenKey);
    }
}
