namespace Steinsiek.Odin.Modules.Core.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations.
/// </summary>
/// <typeparam name="T">The entity type derived from <see cref="BaseEntity"/>.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<T> Add(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated entity.</returns>
    Task<T> Update(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}
