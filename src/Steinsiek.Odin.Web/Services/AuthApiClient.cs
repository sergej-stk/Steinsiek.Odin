namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for authentication API operations.
/// </summary>
public sealed class AuthApiClient(HttpClient httpClient, ILogger<AuthApiClient> logger) : IAuthApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<AuthApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<LoginResponse?> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/login", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Login failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<LoginResponse?> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/register", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Registration failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<UserDto?> GetCurrentUser(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("/api/v1/auth/me", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<UserDto>(cancellationToken);
    }
}
