namespace Steinsiek.Odin.Modules.Auth.Shared.DTOs;

/// <summary>
/// Request payload for assigning a role to a user.
/// </summary>
public sealed record class AssignRoleRequest
{
    /// <summary>
    /// Gets the role identifier to assign.
    /// </summary>
    [Required]
    public required Guid RoleId { get; init; }
}
