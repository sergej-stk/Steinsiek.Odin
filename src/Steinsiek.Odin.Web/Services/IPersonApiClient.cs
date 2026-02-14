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
}
