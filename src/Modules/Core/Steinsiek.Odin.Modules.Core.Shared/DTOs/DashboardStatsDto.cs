namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Data transfer object containing aggregate statistics for the dashboard.
/// </summary>
public sealed record class DashboardStatsDto
{
    /// <summary>
    /// Gets the total number of persons.
    /// </summary>
    public required int TotalPersons { get; init; }

    /// <summary>
    /// Gets the total number of companies.
    /// </summary>
    public required int TotalCompanies { get; init; }

    /// <summary>
    /// Gets the number of active person-company assignments.
    /// </summary>
    public required int ActiveAssignments { get; init; }

    /// <summary>
    /// Gets the number of persons created this month.
    /// </summary>
    public required int NewPersonsThisMonth { get; init; }

    /// <summary>
    /// Gets the number of companies created this month.
    /// </summary>
    public required int NewCompaniesThisMonth { get; init; }
}
