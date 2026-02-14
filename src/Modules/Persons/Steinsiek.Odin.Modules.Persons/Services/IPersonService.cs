namespace Steinsiek.Odin.Modules.Persons.Services;

/// <summary>
/// Defines the contract for person management operations.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Retrieves all persons as a paginated list result.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all persons.</returns>
    Task<ListResult<PersonDto>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a person's detailed information by identifier.
    /// </summary>
    /// <param name="id">The person identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The detailed person data if found; otherwise, null.</returns>
    Task<PersonDetailDto?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new person from the given request.
    /// </summary>
    /// <param name="request">The creation request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created person summary.</returns>
    Task<PersonDto> Create(CreatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing person with the given request data.
    /// </summary>
    /// <param name="id">The identifier of the person to update.</param>
    /// <param name="request">The update request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated person summary if found; otherwise, null.</returns>
    Task<PersonDto?> Update(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a person by identifier.
    /// </summary>
    /// <param name="id">The identifier of the person to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the person was deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches persons by first name or last name.
    /// </summary>
    /// <param name="searchTerm">The search term to match against person names.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing matching persons.</returns>
    Task<ListResult<PersonDto>> Search(string searchTerm, CancellationToken cancellationToken);
}
