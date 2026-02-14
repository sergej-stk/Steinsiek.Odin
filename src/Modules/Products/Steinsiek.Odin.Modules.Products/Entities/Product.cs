namespace Steinsiek.Odin.Modules.Products.Entities;

/// <summary>
/// Represents a product available for sale.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the available stock quantity.
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the product image URL.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the category identifier this product belongs to.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets whether the product is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the category this product belongs to.
    /// </summary>
    public Category? Category { get; set; }
}
