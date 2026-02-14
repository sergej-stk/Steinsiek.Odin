namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for person API operations.
/// </summary>
public sealed class PersonApiClient(
    HttpClient httpClient,
    ITokenStorageService tokenStorage,
    ILogger<PersonApiClient> logger) : IPersonApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenStorageService _tokenStorage = tokenStorage;
    private readonly ILogger<PersonApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>?> GetAll(CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            return await _httpClient.GetFromJsonAsync<ListResult<PersonDto>>("/api/v1/persons", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to retrieve persons");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<PersonDetailDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            return await _httpClient.GetFromJsonAsync<PersonDetailDto>($"/api/v1/persons/{id}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to retrieve person {PersonId}", id);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>?> Search(string searchTerm, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var encoded = Uri.EscapeDataString(searchTerm);
            return await _httpClient.GetFromJsonAsync<ListResult<PersonDto>>($"/api/v1/persons/search?q={encoded}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to search persons with term '{SearchTerm}'", searchTerm);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<PersonDto?> Create(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/persons", request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to create person with status {StatusCode}", response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PersonDto>(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to create person");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<PersonDto?> Update(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/v1/persons/{id}", request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to update person {PersonId} with status {StatusCode}", id, response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PersonDto>(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to update person {PersonId}", id);
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/persons/{id}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to delete person {PersonId} with status {StatusCode}", id, response.StatusCode);
                return false;
            }

            return true;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to delete person {PersonId}", id);
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<PagedResult<PersonDto>?> GetPaged(int page, int pageSize, string? sort, SortDirection? sortDir, string? search, PersonFilterQuery? filter, CancellationToken cancellationToken)
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
                if (!string.IsNullOrWhiteSpace(filter.City))
                {
                    queryParams["city"] = filter.City;
                }

                if (filter.SalutationId.HasValue)
                {
                    queryParams["salutationId"] = filter.SalutationId.Value.ToString();
                }

                if (filter.GenderId.HasValue)
                {
                    queryParams["genderId"] = filter.GenderId.Value.ToString();
                }

                if (filter.NationalityId.HasValue)
                {
                    queryParams["nationalityId"] = filter.NationalityId.Value.ToString();
                }

                if (filter.MaritalStatusId.HasValue)
                {
                    queryParams["maritalStatusId"] = filter.MaritalStatusId.Value.ToString();
                }

                if (filter.CreatedFrom.HasValue)
                {
                    queryParams["createdFrom"] = filter.CreatedFrom.Value.ToString("O");
                }

                if (filter.CreatedTo.HasValue)
                {
                    queryParams["createdTo"] = filter.CreatedTo.Value.ToString("O");
                }
            }

            var queryString = string.Join("&", queryParams
                .Where(kv => kv.Value is not null)
                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value!)}"));

            return await _httpClient.GetFromJsonAsync<PagedResult<PersonDto>>($"/api/v1/persons/paged?{queryString}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to retrieve paged persons");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<int> DeleteMany(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        await ApplyAuthHeader(cancellationToken);
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/persons/bulk")
            {
                Content = JsonContent.Create(ids)
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to bulk delete persons with status {StatusCode}", response.StatusCode);
                return 0;
            }

            return await response.Content.ReadFromJsonAsync<int>(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Failed to bulk delete persons");
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
