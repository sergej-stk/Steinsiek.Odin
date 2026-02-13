namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Provides toast notification functionality.
/// </summary>
public interface IToastService
{
    /// <summary>
    /// Raised when the toast collection changes.
    /// </summary>
    event Action? OnChange;

    /// <summary>
    /// The current list of active toasts.
    /// </summary>
    IReadOnlyList<ToastMessage> Toasts { get; }

    /// <summary>
    /// Displays a new toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="level">The severity level.</param>
    void Show(string message, ToastLevel level);

    /// <summary>
    /// Removes a toast notification by its identifier.
    /// </summary>
    /// <param name="id">The toast identifier.</param>
    void Remove(Guid id);
}
