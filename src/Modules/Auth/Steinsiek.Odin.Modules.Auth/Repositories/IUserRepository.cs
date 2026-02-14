namespace Steinsiek.Odin.Modules.Auth.Repositories;

/// <summary>
/// Repository interface for user entities with email lookup and role loading support.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Retrieves a user by email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the role names assigned to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of role names.</returns>
    Task<IReadOnlyList<string>> GetRoles(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the role was assigned; false if it was already assigned.</returns>
    Task<bool> AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a role from a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the role was removed; false if it was not assigned.</returns>
    Task<bool> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
