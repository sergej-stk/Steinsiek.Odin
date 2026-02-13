namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client for product API operations.
/// </summary>
public interface IProductApiClient
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ListResult<ProductDto>?> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves products by category identifier.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ListResult<ProductDto>?> GetByCategory(Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches products by query string.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ListResult<ProductDto>?> Search(string query, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ProductDto?> Create(CreateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="request">The product update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ProductDto?> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}
