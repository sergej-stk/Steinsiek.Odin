namespace Steinsiek.Odin.Modules.Products.Services;

/// <summary>
/// Service interface for product image operations.
/// </summary>
public interface IProductImageService
{
    /// <summary>
    /// Retrieves the image data for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image data if found; otherwise, null.</returns>
    Task<ProductImageData?> GetByProductId(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    /// Uploads or replaces the image for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="file">The uploaded image file.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Steinsiek.Odin.Modules.Core.Exceptions.ElementNotFoundException">Thrown when the product does not exist.</exception>
    /// <exception cref="Steinsiek.Odin.Modules.Core.Exceptions.OdinValidationException">Thrown when the file type or size is invalid.</exception>
    Task Upload(Guid productId, IFormFile file, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the image for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid productId, CancellationToken cancellationToken);
}
