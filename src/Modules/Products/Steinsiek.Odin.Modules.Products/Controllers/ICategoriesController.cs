namespace Steinsiek.Odin.Modules.Products.Controllers;

/// <summary>
/// Defines the contract for category operations.
/// </summary>
public interface ICategoriesController
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all categories with total count.</returns>
    Task<ActionResult<ListResult<CategoryDto>>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The category if found.</returns>
    Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The category creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created category.</returns>
    Task<ActionResult<CategoryDto>> Create(CreateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="request">The category update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated category if found.</returns>
    Task<ActionResult<CategoryDto>> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if deleted successfully.</returns>
    Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken);
}
