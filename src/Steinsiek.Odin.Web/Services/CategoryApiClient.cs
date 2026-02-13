namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for category API operations.
/// </summary>
public sealed class CategoryApiClient(HttpClient httpClient, ILogger<CategoryApiClient> logger) : ICategoryApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<CategoryApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<CategoryDto>?> GetAll(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ListResult<CategoryDto>>("/api/v1/categories", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/v1/categories/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<CategoryDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/categories", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Category creation failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<CategoryDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/v1/categories/{id}", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Category update failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<CategoryDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"/api/v1/categories/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Category deletion failed with status {StatusCode}", response.StatusCode);
        }

        return response.IsSuccessStatusCode;
    }
}
