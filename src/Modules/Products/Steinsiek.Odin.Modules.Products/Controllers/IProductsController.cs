namespace Steinsiek.Odin.Modules.Products.Controllers;

/// <summary>
/// Defines the contract for product operations.
/// </summary>
public interface IProductsController
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all products with total count.</returns>
    Task<ActionResult<ListResult<ProductDto>>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product if found.</returns>
    Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all products belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing products in the specified category with total count.</returns>
    Task<ActionResult<ListResult<ProductDto>>> GetByCategory(Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for products by name or description.
    /// </summary>
    /// <param name="q">The search query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing matching products with total count.</returns>
    Task<ActionResult<ListResult<ProductDto>>> Search(string? q, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created product.</returns>
    Task<ActionResult<ProductDto>> Create(CreateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="request">The product update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated product if found.</returns>
    Task<ActionResult<ProductDto>> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a product by its identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if deleted successfully.</returns>
    Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken);
}
