namespace Steinsiek.Odin.Modules.Products.Shared.DTOs;

/// <summary>
/// Data transfer object for category information.
/// </summary>
public sealed record class CategoryDto
{
    /// <summary>
    /// The category identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The category name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The category description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Whether the category is active.
    /// </summary>
    public required bool IsActive { get; init; }
}

/// <summary>
/// Request payload for creating a new category.
/// </summary>
public sealed record class CreateCategoryRequest
{
    /// <summary>
    /// The category name (minimum 2 characters).
    /// </summary>
    [Required, MinLength(2)]
    public required string Name { get; init; }

    /// <summary>
    /// The category description.
    /// </summary>
    public string? Description { get; init; }
}

/// <summary>
/// Request payload for updating an existing category.
/// </summary>
public sealed record class UpdateCategoryRequest
{
    /// <summary>
    /// The category name (minimum 2 characters).
    /// </summary>
    [Required, MinLength(2)]
    public required string Name { get; init; }

    /// <summary>
    /// The category description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Whether the category is active.
    /// </summary>
    public required bool IsActive { get; init; }
}
