namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Wrapper for collection responses including total count for pagination.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public sealed record class ListResult<T> where T : notnull
{
    /// <summary>
    /// The total number of elements matching the query.
    /// </summary>
    public required int TotalCount { get; init; }

    /// <summary>
    /// The collection of elements for the current page.
    /// </summary>
    public required IReadOnlyList<T> Data { get; init; }
}
