namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Filter parameters for company list queries.
/// </summary>
public sealed record class CompanyFilterQuery
{
    /// <summary>
    /// Filters by industry lookup identifier.
    /// </summary>
    public Guid? IndustryId { get; init; }

    /// <summary>
    /// Filters by legal form lookup identifier.
    /// </summary>
    public Guid? LegalFormId { get; init; }

    /// <summary>
    /// Filters by city (partial match on primary location).
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Filters companies with at least this many employees.
    /// </summary>
    public int? EmployeeCountMin { get; init; }

    /// <summary>
    /// Filters companies with at most this many employees.
    /// </summary>
    public int? EmployeeCountMax { get; init; }

    /// <summary>
    /// Filters companies founded on or after this date.
    /// </summary>
    public DateTime? FoundingDateFrom { get; init; }

    /// <summary>
    /// Filters companies founded on or before this date.
    /// </summary>
    public DateTime? FoundingDateTo { get; init; }
}
