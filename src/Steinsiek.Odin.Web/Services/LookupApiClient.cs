namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for lookup API operations.
/// </summary>
public sealed class LookupApiClient(
    HttpClient httpClient,
    ITokenStorageService tokenStorage,
    ILogger<LookupApiClient> logger) : ILookupApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenStorageService _tokenStorage = tokenStorage;
    private readonly ILogger<LookupApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<IReadOnlyList<LookupDto>> GetByType(string type, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var result = await _httpClient.GetFromJsonAsync<ListResult<LookupDto>>($"/api/v1/lookups/{Uri.EscapeDataString(type)}", cancellationToken);
            return result?.Data ?? [];
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to retrieve lookups for type '{LookupType}'", type);
            return [];
        }
    }

    /// <summary>
    /// Applies the JWT Bearer token from session storage to the HTTP client.
    /// </summary>
    private async Task ApplyAuthHeader(CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetToken(cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
