namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Specifies the sort direction for paginated queries.
/// </summary>
public enum SortDirection
{
    /// <summary>
    /// Ascending order (A-Z, 0-9, oldest first).
    /// </summary>
    Asc,

    /// <summary>
    /// Descending order (Z-A, 9-0, newest first).
    /// </summary>
    Desc
}
