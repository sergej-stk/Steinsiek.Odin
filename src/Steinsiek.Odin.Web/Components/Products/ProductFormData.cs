namespace Steinsiek.Odin.Web.Components.Products;

/// <summary>
/// Holds the form data submitted from the product form.
/// </summary>
public sealed class ProductFormData
{
    /// <summary>
    /// The product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The product description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The product stock quantity.
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// The product image URL.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The selected category identifier.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Whether the product is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
