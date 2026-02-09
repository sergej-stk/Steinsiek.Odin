namespace Steinsiek.Odin.API.Tests;

/// <summary>
/// Integration tests for the Products API endpoints.
/// </summary>
[TestClass]
public sealed class ProductsEndpointTests
{
    private static HttpClient Client => TestSetup.Client;

    /// <summary>
    /// Verifies that GET /api/v1/products returns 200 OK with a ListResult.
    /// </summary>
    [TestMethod]
    public async Task GetAllProducts_Returns200WithListResult()
    {
        var response = await Client.GetAsync("/api/v1/products");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListResult<ProductDto>>();
        Assert.IsNotNull(result);
        Assert.IsGreaterThan(0, result.TotalCount);
        Assert.IsNotNull(result.Data);
        Assert.HasCount(result.TotalCount, result.Data);
    }

    /// <summary>
    /// Verifies that GET /api/v1/products/{id} returns 200 OK for an existing product.
    /// </summary>
    [TestMethod]
    public async Task GetProductById_ExistingProduct_Returns200()
    {
        var listResponse = await Client.GetFromJsonAsync<ListResult<ProductDto>>("/api/v1/products");
        Assert.IsNotNull(listResponse);
        Assert.IsNotEmpty(listResponse.Data);

        var firstProduct = listResponse.Data[0];
        var response = await Client.GetAsync($"/api/v1/products/{firstProduct.Id}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();
        Assert.IsNotNull(product);
        Assert.AreEqual(firstProduct.Id, product.Id);
    }

    /// <summary>
    /// Verifies that GET /api/v1/products/{id} returns 404 for a non-existent product.
    /// </summary>
    [TestMethod]
    public async Task GetProductById_NonExistent_Returns404()
    {
        var response = await Client.GetAsync($"/api/v1/products/{Guid.NewGuid()}");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Verifies that POST /api/v1/products returns 401 without authentication.
    /// </summary>
    [TestMethod]
    public async Task CreateProduct_WithoutAuth_Returns401()
    {
        var request = new CreateProductRequest
        {
            Name = "Test Product",
            Description = "Test",
            Price = 10m,
            Stock = 5,
            CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
        };

        var response = await Client.PostAsJsonAsync("/api/v1/products", request);

        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    /// <summary>
    /// Verifies that GET /api/v1/products/search returns results for a matching term.
    /// </summary>
    [TestMethod]
    public async Task SearchProducts_ReturnsListResult()
    {
        var response = await Client.GetAsync("/api/v1/products/search?q=a");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListResult<ProductDto>>();
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
    }

    /// <summary>
    /// Verifies that GET /api/v1/products/search returns empty result for blank query.
    /// </summary>
    [TestMethod]
    public async Task SearchProducts_EmptyQuery_ReturnsEmptyListResult()
    {
        var response = await Client.GetAsync("/api/v1/products/search?q=");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListResult<ProductDto>>();
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.TotalCount);
    }
}
