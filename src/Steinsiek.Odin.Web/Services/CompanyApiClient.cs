namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for company API operations.
/// </summary>
public sealed class CompanyApiClient(
    HttpClient httpClient,
    ITokenStorageService tokenStorage,
    ILogger<CompanyApiClient> logger) : ICompanyApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenStorageService _tokenStorage = tokenStorage;
    private readonly ILogger<CompanyApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<CompanyDto>?> GetAll(CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.GetAsync("/api/v1/companies", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to retrieve companies with status {StatusCode}", response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ListResult<CompanyDto>>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving companies");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<CompanyDetailDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.GetAsync($"/api/v1/companies/{id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to retrieve company {CompanyId} with status {StatusCode}", id, response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CompanyDetailDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving company {CompanyId}", id);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ListResult<CompanyDto>?> Search(string searchTerm, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.GetAsync($"/api/v1/companies/search?q={Uri.EscapeDataString(searchTerm)}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to search companies with status {StatusCode}", response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ListResult<CompanyDto>>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching companies with term '{SearchTerm}'", searchTerm);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<CompanyDto?> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/companies", request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create company with status {StatusCode}", response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CompanyDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating company");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<CompanyDto?> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/v1/companies/{id}", request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update company {CompanyId} with status {StatusCode}", id, response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CompanyDto>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating company {CompanyId}", id);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/companies/{id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete company {CompanyId} with status {StatusCode}", id, response.StatusCode);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting company {CompanyId}", id);
            return false;
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
