namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for accessing dashboard API endpoints.
/// </summary>
public sealed class DashboardApiClient(
    HttpClient httpClient,
    ITokenStorageService tokenStorage,
    ILogger<DashboardApiClient> logger) : IDashboardApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenStorageService _tokenStorage = tokenStorage;
    private readonly ILogger<DashboardApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<DashboardStatsDto?> GetStats(CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            return await _httpClient.GetFromJsonAsync<DashboardStatsDto>(
                "api/v1/dashboard/stats", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to fetch dashboard stats");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ListResult<AuditLogDto>?> GetRecentActivity(int count, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            return await _httpClient.GetFromJsonAsync<ListResult<AuditLogDto>>(
                $"api/v1/dashboard/recent-activity?count={count}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to fetch recent activity");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>?> GetUpcomingBirthdays(int days, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            return await _httpClient.GetFromJsonAsync<ListResult<PersonDto>>(
                $"api/v1/dashboard/upcoming-birthdays?days={days}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to fetch upcoming birthdays");
            return null;
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
