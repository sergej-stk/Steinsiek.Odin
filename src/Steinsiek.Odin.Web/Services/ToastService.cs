namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Manages toast notifications with automatic dismissal after 5 seconds.
/// </summary>
public sealed class ToastService : IToastService, IDisposable
{
    private readonly List<ToastMessage> _toasts = [];
    private readonly Dictionary<Guid, Timer> _timers = [];

    /// <inheritdoc />
    public event Action? OnChange;

    /// <inheritdoc />
    public IReadOnlyList<ToastMessage> Toasts => _toasts.AsReadOnly();

    /// <inheritdoc />
    public void Show(string message, ToastLevel level)
    {
        var toast = new ToastMessage { Message = message, Level = level };
        _toasts.Add(toast);

        var timer = new Timer(_ => Remove(toast.Id), null, 5000, Timeout.Infinite);
        _timers[toast.Id] = timer;

        OnChange?.Invoke();
    }

    /// <inheritdoc />
    public void Remove(Guid id)
    {
        var toast = _toasts.FirstOrDefault(t => t.Id == id);
        if (toast is not null)
        {
            _toasts.Remove(toast);
        }

        if (_timers.Remove(id, out var timer))
        {
            timer.Dispose();
        }

        OnChange?.Invoke();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        foreach (var timer in _timers.Values)
        {
            timer.Dispose();
        }

        _timers.Clear();
    }
}
