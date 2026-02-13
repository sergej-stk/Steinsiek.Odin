namespace Steinsiek.Odin.Modules.Products.Controllers;

/// <summary>
/// Defines the contract for product image operations.
/// </summary>
public interface IProductImagesController
{
    /// <summary>
    /// Retrieves the image for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The image file or 404.</returns>
    Task<IActionResult> GetImage(Guid productId, CancellationToken cancellationToken);

    /// <summary>
    /// Uploads or replaces the image for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="file">The image file to upload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content on success.</returns>
    Task<IActionResult> UploadImage(Guid productId, IFormFile file, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the image for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content on success or 404.</returns>
    Task<IActionResult> DeleteImage(Guid productId, CancellationToken cancellationToken);
}
