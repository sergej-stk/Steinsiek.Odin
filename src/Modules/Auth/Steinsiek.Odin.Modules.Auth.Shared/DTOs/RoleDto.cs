namespace Steinsiek.Odin.Modules.Auth.Shared.DTOs;

/// <summary>
/// Data transfer object for a role.
/// </summary>
public sealed record class RoleDto
{
    /// <summary>
    /// Gets the role identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the role name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the role description.
    /// </summary>
    public string? Description { get; init; }
}
