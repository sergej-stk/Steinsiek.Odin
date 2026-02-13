namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client implementation for product API operations.
/// </summary>
public sealed class ProductApiClient(HttpClient httpClient, ILogger<ProductApiClient> logger) : IProductApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ProductApiClient> _logger = logger;

    /// <inheritdoc />
    public async Task<ListResult<ProductDto>?> GetAll(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ListResult<ProductDto>>("/api/v1/products", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/api/v1/products/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ListResult<ProductDto>?> GetByCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ListResult<ProductDto>>(
            $"/api/v1/products/category/{categoryId}", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ListResult<ProductDto>?> Search(string query, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<ListResult<ProductDto>>(
            $"/api/v1/products/search?q={Uri.EscapeDataString(query)}", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductDto?> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/v1/products", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Product creation failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductDto?> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/v1/products/{id}", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Product update failed with status {StatusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"/api/v1/products/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Product deletion failed with status {StatusCode}", response.StatusCode);
        }

        return response.IsSuccessStatusCode;
    }
}
