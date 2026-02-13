namespace Steinsiek.Odin.Web.Components.Products;

/// <summary>
/// Holds the form data submitted from the product form.
/// </summary>
public sealed class ProductFormData
{
    /// <summary>
    /// The product name.
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The product description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The product price.
    /// </summary>
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    /// <summary>
    /// The product stock quantity.
    /// </summary>
    [Required(ErrorMessage = "Stock is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater")]
    public int Stock { get; set; }

    /// <summary>
    /// The product image URL.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The selected category identifier.
    /// </summary>
    [Required(ErrorMessage = "Category is required")]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Whether the product is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
