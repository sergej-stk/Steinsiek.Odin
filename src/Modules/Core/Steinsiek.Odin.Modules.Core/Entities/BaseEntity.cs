namespace Steinsiek.Odin.Modules.Core.Entities;

/// <summary>
/// Base class for all domain entities providing common audit properties.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last update timestamp in UTC, or null if never updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
