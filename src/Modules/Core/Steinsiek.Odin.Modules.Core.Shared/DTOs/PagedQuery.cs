namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Common query parameters for server-side paginated, sorted, and searchable endpoints.
/// </summary>
public sealed record class PagedQuery
{
    /// <summary>
    /// The one-based page number to retrieve.
    /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// The number of items per page (25, 50, or 100).
    /// </summary>
    public int PageSize { get; init; } = 25;

    /// <summary>
    /// The column name to sort by. Validated by each module's service layer.
    /// </summary>
    public string? Sort { get; init; }

    /// <summary>
    /// The sort direction.
    /// </summary>
    public SortDirection? SortDir { get; init; }

    /// <summary>
    /// The full-text search term.
    /// </summary>
    public string? Q { get; init; }
}
