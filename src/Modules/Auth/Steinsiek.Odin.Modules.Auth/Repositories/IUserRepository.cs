namespace Steinsiek.Odin.Modules.Auth.Repositories;

/// <summary>
/// Repository interface for user entities with email lookup support.
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
}
