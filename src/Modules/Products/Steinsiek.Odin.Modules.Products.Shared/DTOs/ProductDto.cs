namespace Steinsiek.Odin.Modules.Products.Shared.DTOs;

/// <summary>
/// Data transfer object for product information.
/// </summary>
public sealed record class ProductDto
{
    /// <summary>
    /// The product identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The product name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The product description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The product price.
    /// </summary>
    public required decimal Price { get; init; }

    /// <summary>
    /// The available stock quantity.
    /// </summary>
    public required int Stock { get; init; }

    /// <summary>
    /// The product image URL.
    /// </summary>
    public string? ImageUrl { get; init; }

    /// <summary>
    /// The category identifier.
    /// </summary>
    public required Guid CategoryId { get; init; }

    /// <summary>
    /// The category name.
    /// </summary>
    public string? CategoryName { get; init; }

    /// <summary>
    /// Whether the product is active.
    /// </summary>
    public required bool IsActive { get; init; }
}

/// <summary>
/// Request payload for creating a new product.
/// </summary>
public sealed record class CreateProductRequest
{
    /// <summary>
    /// The product name (minimum 2 characters).
    /// </summary>
    [Required, MinLength(2)]
    public required string Name { get; init; }

    /// <summary>
    /// The product description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The product price (must be greater than 0).
    /// </summary>
    [Required, Range(0.01, double.MaxValue)]
    public required decimal Price { get; init; }

    /// <summary>
    /// The available stock quantity (must be non-negative).
    /// </summary>
    [Required, Range(0, int.MaxValue)]
    public required int Stock { get; init; }

    /// <summary>
    /// The product image URL.
    /// </summary>
    public string? ImageUrl { get; init; }

    /// <summary>
    /// The category identifier.
    /// </summary>
    [Required]
    public required Guid CategoryId { get; init; }
}

/// <summary>
/// Request payload for updating an existing product.
/// </summary>
public sealed record class UpdateProductRequest
{
    /// <summary>
    /// The product name (minimum 2 characters).
    /// </summary>
    [Required, MinLength(2)]
    public required string Name { get; init; }

    /// <summary>
    /// The product description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The product price (must be greater than 0).
    /// </summary>
    [Required, Range(0.01, double.MaxValue)]
    public required decimal Price { get; init; }

    /// <summary>
    /// The available stock quantity (must be non-negative).
    /// </summary>
    [Required, Range(0, int.MaxValue)]
    public required int Stock { get; init; }

    /// <summary>
    /// The product image URL.
    /// </summary>
    public string? ImageUrl { get; init; }

    /// <summary>
    /// The category identifier.
    /// </summary>
    [Required]
    public required Guid CategoryId { get; init; }

    /// <summary>
    /// Whether the product is active.
    /// </summary>
    public required bool IsActive { get; init; }
}
