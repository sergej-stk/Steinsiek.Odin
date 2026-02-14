namespace Steinsiek.Odin.Modules.Core.Entities;

/// <summary>
/// Represents a single audit log entry recording a change to a tracked entity.
/// Does not inherit from <see cref="BaseEntity"/> as audit entries are never soft-deleted or audited themselves.
/// </summary>
public sealed class AuditLogEntry
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the CLR type name of the audited entity.
    /// </summary>
    public required string EntityType { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the audited entity.
    /// </summary>
    public required Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the action performed (Created, Updated, Deleted).
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// Gets or sets the name of the changed property, or null for Created/Deleted actions.
    /// </summary>
    public string? PropertyName { get; set; }

    /// <summary>
    /// Gets or sets the old value before the change, or null for Created actions.
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// Gets or sets the new value after the change, or null for Deleted actions.
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who performed the action.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the action was performed.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
