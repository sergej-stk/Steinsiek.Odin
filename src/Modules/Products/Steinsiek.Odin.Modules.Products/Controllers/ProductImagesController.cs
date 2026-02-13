namespace Steinsiek.Odin.Modules.Products.Controllers;

/// <summary>
/// API controller for product image operations.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/products/{productId:guid}/image")]
[Authorize]
public sealed class ProductImagesController(IProductImageService productImageService) : ControllerBase, IProductImagesController
{
    private readonly IProductImageService _productImageService = productImageService;

    /// <inheritdoc />
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage(Guid productId, CancellationToken cancellationToken)
    {
        var imageData = await _productImageService.GetByProductId(productId, cancellationToken);
        if (imageData is null)
        {
            return NotFound();
        }

        return File(imageData.Data, imageData.ContentType, imageData.FileName);
    }

    /// <inheritdoc />
    [HttpPut]
    [RequestSizeLimit(5_242_880)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadImage(Guid productId, IFormFile file, CancellationToken cancellationToken)
    {
        await _productImageService.Upload(productId, file, cancellationToken);
        return NoContent();
    }

    /// <inheritdoc />
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(Guid productId, CancellationToken cancellationToken)
    {
        var result = await _productImageService.Delete(productId, cancellationToken);
        return !result ? NotFound() : NoContent();
    }
}
