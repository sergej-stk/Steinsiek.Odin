namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// Service interface for authentication and role management operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful; otherwise, null.</returns>
    Task<LoginResponse?> Login(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful; otherwise, null.</returns>
    Task<LoginResponse?> Register(RegisterRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all available roles.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of available roles.</returns>
    Task<IReadOnlyList<RoleDto>> GetAllRoles(CancellationToken cancellationToken);

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the role was assigned; false if already assigned.</returns>
    Task<bool> AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a role from a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the role was removed; false if not assigned.</returns>
    Task<bool> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
