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

    /// <inheritdoc />
    public async Task<PagedResult<CompanyDto>?> GetPaged(int page, int pageSize, string? sort, SortDirection? sortDir, string? search, CompanyFilterQuery? filter, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["page"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (!string.IsNullOrWhiteSpace(sort))
            {
                queryParams["sort"] = sort;
            }

            if (sortDir.HasValue)
            {
                queryParams["sortDir"] = sortDir.Value.ToString();
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queryParams["q"] = search;
            }

            if (filter is not null)
            {
                if (filter.IndustryId.HasValue)
                {
                    queryParams["industryId"] = filter.IndustryId.Value.ToString();
                }

                if (filter.LegalFormId.HasValue)
                {
                    queryParams["legalFormId"] = filter.LegalFormId.Value.ToString();
                }

                if (!string.IsNullOrWhiteSpace(filter.City))
                {
                    queryParams["city"] = filter.City;
                }

                if (filter.EmployeeCountMin.HasValue)
                {
                    queryParams["employeeCountMin"] = filter.EmployeeCountMin.Value.ToString();
                }

                if (filter.EmployeeCountMax.HasValue)
                {
                    queryParams["employeeCountMax"] = filter.EmployeeCountMax.Value.ToString();
                }

                if (filter.FoundingDateFrom.HasValue)
                {
                    queryParams["foundingDateFrom"] = filter.FoundingDateFrom.Value.ToString("O");
                }

                if (filter.FoundingDateTo.HasValue)
                {
                    queryParams["foundingDateTo"] = filter.FoundingDateTo.Value.ToString("O");
                }
            }

            var queryString = string.Join("&", queryParams
                .Where(kv => kv.Value is not null)
                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value!)}"));

            return await _httpClient.GetFromJsonAsync<PagedResult<CompanyDto>>($"/api/v1/companies/paged?{queryString}", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paged companies");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<int> DeleteMany(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/companies/bulk")
            {
                Content = JsonContent.Create(ids)
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to bulk delete companies with status {StatusCode}", response.StatusCode);
                return 0;
            }

            return await response.Content.ReadFromJsonAsync<int>(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error bulk deleting companies");
            return 0;
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
