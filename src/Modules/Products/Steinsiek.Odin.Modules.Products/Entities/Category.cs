namespace Steinsiek.Odin.Modules.Products.Entities;

/// <summary>
/// Represents a product category for grouping related products.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets whether the category is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
