namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Wrapper for paginated collection responses with metadata.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public sealed record class PagedResult<T> where T : notnull
{
    /// <summary>
    /// The total number of elements matching the query across all pages.
    /// </summary>
    public required int TotalCount { get; init; }

    /// <summary>
    /// The collection of elements for the current page.
    /// </summary>
    public required IReadOnlyList<T> Data { get; init; }

    /// <summary>
    /// The current one-based page number.
    /// </summary>
    public required int CurrentPage { get; init; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public required int PageSize { get; init; }

    /// <summary>
    /// The total number of pages.
    /// </summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}
