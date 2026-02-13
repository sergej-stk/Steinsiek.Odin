namespace Steinsiek.Odin.Modules.Products.Services;

/// <summary>
/// Implementation of the product image service handling upload, retrieval, and deletion.
/// </summary>
/// <param name="productImageRepository">The product image repository.</param>
/// <param name="productRepository">The product repository.</param>
/// <param name="logger">The logger instance.</param>
public sealed class ProductImageService(
    IProductImageRepository productImageRepository,
    IProductRepository productRepository,
    ILogger<ProductImageService> logger) : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository = productImageRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ILogger<ProductImageService> _logger = logger;

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    private static readonly HashSet<string> _allowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    /// <inheritdoc />
    public async Task<ProductImageData?> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await _productImageRepository.GetByProductId(productId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task Upload(Guid productId, IFormFile file, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(productId, cancellationToken);
        if (product is null)
        {
            throw new ElementNotFoundException("Product", productId);
        }

        if (!_allowedContentTypes.Contains(file.ContentType))
        {
            throw new OdinValidationException($"Content type '{file.ContentType}' is not allowed. Allowed types: {string.Join(", ", _allowedContentTypes)}");
        }

        if (file.Length > MaxFileSizeBytes)
        {
            throw new OdinValidationException($"File size {file.Length} bytes exceeds maximum allowed size of {MaxFileSizeBytes} bytes (5 MB)");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var imageData = new ProductImageData
        {
            ProductId = productId,
            ContentType = file.ContentType,
            FileName = file.FileName,
            Data = memoryStream.ToArray()
        };

        await _productImageRepository.Upsert(imageData, cancellationToken);
        _logger.LogInformation("Image uploaded for product {ProductId}: {FileName} ({ContentType}, {Size} bytes)",
            productId, file.FileName, file.ContentType, file.Length);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid productId, CancellationToken cancellationToken)
    {
        var result = await _productImageRepository.Delete(productId, cancellationToken);
        if (result)
        {
            _logger.LogInformation("Image deleted for product {ProductId}", productId);
        }
        return result;
    }
}
