namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Repository interface for product image binary data.
/// </summary>
public interface IProductImageRepository
{
    /// <summary>
    /// Retrieves the image data for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image data if found; otherwise, null.</returns>
    Task<ProductImageData?> GetByProductId(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates or replaces the image data for a product.
    /// </summary>
    /// <param name="imageData">The image data to store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Upsert(ProductImageData imageData, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the image data for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether an image exists for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if an image exists; otherwise, false.</returns>
    Task<bool> Exists(Guid productId, CancellationToken cancellationToken);
}
