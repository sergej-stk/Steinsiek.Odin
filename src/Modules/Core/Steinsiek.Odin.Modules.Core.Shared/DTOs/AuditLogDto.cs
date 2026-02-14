namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Data transfer object for audit log entries.
/// </summary>
public sealed record class AuditLogDto
{
    /// <summary>
    /// Gets the unique identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the CLR type name of the audited entity.
    /// </summary>
    public required string EntityType { get; init; }

    /// <summary>
    /// Gets the identifier of the audited entity.
    /// </summary>
    public required Guid EntityId { get; init; }

    /// <summary>
    /// Gets the action performed.
    /// </summary>
    public required string Action { get; init; }

    /// <summary>
    /// Gets the name of the changed property.
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Gets the old value before the change.
    /// </summary>
    public string? OldValue { get; init; }

    /// <summary>
    /// Gets the new value after the change.
    /// </summary>
    public string? NewValue { get; init; }

    /// <summary>
    /// Gets the identifier of the user who performed the action.
    /// </summary>
    public Guid? UserId { get; init; }

    /// <summary>
    /// Gets the UTC timestamp when the action was performed.
    /// </summary>
    public required DateTime Timestamp { get; init; }
}
