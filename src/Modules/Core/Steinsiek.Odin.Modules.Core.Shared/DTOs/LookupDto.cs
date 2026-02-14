namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Represents a translated lookup entry for API responses.
/// </summary>
public sealed record class LookupDto
{
    /// <summary>
    /// Gets the unique identifier of the lookup entry.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Gets the unique code of the lookup entry.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Gets the translated display value.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Gets the sort order for display purposes.
    /// </summary>
    public required int SortOrder { get; init; }
}
