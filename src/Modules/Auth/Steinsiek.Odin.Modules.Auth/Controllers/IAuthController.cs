namespace Steinsiek.Odin.Modules.Auth.Controllers;

/// <summary>
/// Defines the contract for authentication and role management operations.
/// </summary>
public interface IAuthController
{
    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful.</returns>
    Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Registers a new user account and returns a JWT token.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The login response with JWT token if successful.</returns>
    Task<ActionResult<LoginResponse>> Register(RegisterRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns the current authenticated user's information.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The current user's details.</returns>
    Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all available roles.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of available roles.</returns>
    Task<ActionResult<IReadOnlyList<RoleDto>>> GetRoles(CancellationToken cancellationToken);

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="request">The role assignment request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    Task<IActionResult> AssignRole(Guid userId, AssignRoleRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a role from a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="roleId">The role identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content if successful.</returns>
    Task<IActionResult> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken);
}
