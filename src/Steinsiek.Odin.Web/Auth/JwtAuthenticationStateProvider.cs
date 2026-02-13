namespace Steinsiek.Odin.Web.Auth;

/// <summary>
/// Provides authentication state by parsing JWT tokens from secure storage.
/// </summary>
public sealed class JwtAuthenticationStateProvider(ITokenStorageService tokenStorage) : AuthenticationStateProvider
{
    private static readonly AuthenticationState _anonymousState =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    private readonly ITokenStorageService _tokenStorage = tokenStorage;

    /// <inheritdoc />
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetToken(CancellationToken.None);
        if (string.IsNullOrEmpty(token))
        {
            return _anonymousState;
        }

        var claims = ParseClaimsFromJwt(token);
        if (claims is null || claims.Count == 0)
        {
            return _anonymousState;
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    /// <summary>
    /// Notifies the authentication system that the authentication state has changed.
    /// </summary>
    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static List<Claim>? ParseClaimsFromJwt(string token)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                return null;
            }

            var payload = PadBase64(parts[1]);
            var jsonBytes = Convert.FromBase64String(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);
            if (keyValuePairs is null)
            {
                return null;
            }

            var claims = new List<Claim>();
            foreach (var kvp in keyValuePairs)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty));
            }

            return claims;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string PadBase64(string base64)
    {
        var output = base64.Replace('-', '+').Replace('_', '/');
        return (output.Length % 4) switch
        {
            2 => output + "==",
            3 => output + "=",
            _ => output
        };
    }
}
