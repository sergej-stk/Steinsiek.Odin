namespace Steinsiek.Odin.Web.Components.Shared;

/// <summary>
/// UI model representing an active filter displayed as a removable chip.
/// </summary>
public sealed record class FilterChipModel
{
    /// <summary>
    /// The filter parameter key used for identification and removal.
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// The human-readable label describing the filter.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The display value of the active filter.
    /// </summary>
    public required string Value { get; init; }
}
