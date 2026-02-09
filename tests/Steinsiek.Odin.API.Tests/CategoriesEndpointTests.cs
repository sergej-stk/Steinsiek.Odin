namespace Steinsiek.Odin.API.Tests;

/// <summary>
/// Integration tests for the Categories API endpoints.
/// </summary>
[TestClass]
public sealed class CategoriesEndpointTests
{
    private static HttpClient Client => TestSetup.Client;

    /// <summary>
    /// Verifies that GET /api/v1/categories returns 200 OK with a ListResult.
    /// </summary>
    [TestMethod]
    public async Task GetAllCategories_Returns200WithListResult()
    {
        var response = await Client.GetAsync("/api/v1/categories");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ListResult<CategoryDto>>();
        Assert.IsNotNull(result);
        Assert.IsGreaterThan(0, result.TotalCount);
        Assert.IsNotNull(result.Data);
    }

    /// <summary>
    /// Verifies that GET /api/v1/categories/{id} returns 200 OK for an existing category.
    /// </summary>
    [TestMethod]
    public async Task GetCategoryById_ExistingCategory_Returns200()
    {
        var knownCategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var response = await Client.GetAsync($"/api/v1/categories/{knownCategoryId}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var category = await response.Content.ReadFromJsonAsync<CategoryDto>();
        Assert.IsNotNull(category);
        Assert.AreEqual(knownCategoryId, category.Id);
    }

    /// <summary>
    /// Verifies that GET /api/v1/categories/{id} returns 404 for a non-existent category.
    /// </summary>
    [TestMethod]
    public async Task GetCategoryById_NonExistent_Returns404()
    {
        var response = await Client.GetAsync($"/api/v1/categories/{Guid.NewGuid()}");

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Verifies that POST /api/v1/categories returns 401 without authentication.
    /// </summary>
    [TestMethod]
    public async Task CreateCategory_WithoutAuth_Returns401()
    {
        var request = new CreateCategoryRequest
        {
            Name = "Test Category",
            Description = "Test"
        };

        var response = await Client.PostAsJsonAsync("/api/v1/categories", request);

        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
