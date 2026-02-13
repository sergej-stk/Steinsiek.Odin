namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// HTTP client for category API operations.
/// </summary>
public interface ICategoryApiClient
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ListResult<CategoryDto>?> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The category creation request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<CategoryDto?> Create(CreateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="request">The category update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<CategoryDto?> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}
