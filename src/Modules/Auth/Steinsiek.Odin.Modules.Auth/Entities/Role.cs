namespace Steinsiek.Odin.Modules.Auth.Entities;

/// <summary>
/// Represents an authorization role in the system.
/// </summary>
public sealed class Role : BaseEntity
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the role description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the user-role assignments for this role.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
