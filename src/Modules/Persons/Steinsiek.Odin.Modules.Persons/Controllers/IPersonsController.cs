namespace Steinsiek.Odin.Modules.Persons.Controllers;

/// <summary>
/// Defines the contract for person management API operations.
/// </summary>
public interface IPersonsController
{
    /// <summary>
    /// Retrieves all persons.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing all persons.</returns>
    Task<ActionResult<ListResult<PersonDto>>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a person's detailed information by identifier.
    /// </summary>
    /// <param name="id">The person identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The detailed person data.</returns>
    Task<ActionResult<PersonDetailDto>> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches persons by name.
    /// </summary>
    /// <param name="q">The search term.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result containing matching persons.</returns>
    Task<ActionResult<ListResult<PersonDto>>> Search(string? q, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="request">The creation request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created person summary.</returns>
    Task<ActionResult<PersonDto>> Create(CreatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The person identifier.</param>
    /// <param name="request">The update request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated person summary.</returns>
    Task<ActionResult<PersonDto>> Update(Guid id, UpdatePersonRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a person by identifier.
    /// </summary>
    /// <param name="id">The person identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken);
}
