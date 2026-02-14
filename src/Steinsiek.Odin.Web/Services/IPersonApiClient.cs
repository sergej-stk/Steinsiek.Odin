namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Client interface for person API operations.
/// </summary>
public interface IPersonApiClient
{
    /// <summary>
    /// Retrieves all persons.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list result of persons, or <c>null</c> if the request failed.</returns>
    Task<ListResult<PersonDto>?> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a person by identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The person detail, or <c>null</c> if not found.</returns>
    Task<PersonDetailDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches persons by term.
    /// </summary>
    /// <param name="searchTerm">The search term to filter persons.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list result of matching persons, or <c>null</c> if the request failed.</returns>
    Task<ListResult<PersonDto>?> Search(string searchTerm, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="request">The creation request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created person, or <c>null</c> if creation failed.</returns>
    Task<PersonDto?> Create(CreatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The unique identifier of the person to update.</param>
    /// <param name="request">The update request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated person, or <c>null</c> if the update failed.</returns>
    Task<PersonDto?> Update(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a person.
    /// </summary>
    /// <param name="id">The unique identifier of the person to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if the person was deleted; otherwise, <c>false</c>.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paged, filtered, and sorted list of persons.
    /// </summary>
    /// <param name="page">The one-based page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="sort">The sort column name.</param>
    /// <param name="sortDir">The sort direction.</param>
    /// <param name="search">The full-text search term.</param>
    /// <param name="filter">The filter criteria.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The paged result, or <c>null</c> if the request failed.</returns>
    Task<PagedResult<PersonDto>?> GetPaged(int page, int pageSize, string? sort, SortDirection? sortDir, string? search, PersonFilterQuery? filter, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes multiple persons by their identifiers.
    /// </summary>
    /// <param name="ids">The collection of person identifiers to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of successfully deleted persons.</returns>
    Task<int> DeleteMany(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);
}
