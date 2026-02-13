namespace Steinsiek.Odin.Modules.Products.Entities;

/// <summary>
/// Represents binary image data associated with a product.
/// </summary>
public sealed class ProductImageData
{
    /// <summary>
    /// Gets or sets the identifier of the product this image belongs to.
    /// </summary>
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the MIME content type of the image.
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the original file name of the image.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the raw image bytes.
    /// </summary>
    public required byte[] Data { get; set; }
}
