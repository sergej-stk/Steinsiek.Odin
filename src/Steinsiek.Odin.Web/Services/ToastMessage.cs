namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Represents a single toast notification message.
/// </summary>
public sealed class ToastMessage
{
    /// <summary>
    /// The unique identifier for this toast.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// The message text to display.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The severity level of the toast.
    /// </summary>
    public required ToastLevel Level { get; init; }

    /// <summary>
    /// The time this toast was created.
    /// </summary>
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}
