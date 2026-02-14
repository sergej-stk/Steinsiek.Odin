namespace Steinsiek.Odin.Modules.Auth.Entities;

/// <summary>
/// Junction entity representing the assignment of a role to a user.
/// </summary>
public sealed class UserRole : BaseEntity
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Gets or sets the associated user.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Gets or sets the associated role.
    /// </summary>
    public Role? Role { get; set; }
}
