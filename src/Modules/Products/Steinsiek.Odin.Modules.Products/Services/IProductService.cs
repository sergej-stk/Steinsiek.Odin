namespace Steinsiek.Odin.Modules.Products.Services;

/// <summary>
/// Service interface for product operations.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of all products.</returns>
    Task<IEnumerable<ProductDto>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    Task<ProductDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all products belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of products in the specified category.</returns>
    Task<IEnumerable<ProductDto>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for products by name or description.
    /// </summary>
    /// <param name="searchTerm">The search term to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of matching products.</returns>
    Task<IEnumerable<ProductDto>> Search(string searchTerm, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created product.</returns>
    Task<ProductDto> Create(CreateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="request">The product update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated product if found; otherwise, null.</returns>
    Task<ProductDto?> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}
