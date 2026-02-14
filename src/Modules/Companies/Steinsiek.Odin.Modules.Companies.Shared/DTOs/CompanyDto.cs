namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Summary data transfer object for a company.
/// </summary>
public sealed record class CompanyDto
{
    /// <summary>
    /// The unique identifier of the company.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The name of the company.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The legal form of the company, or null if not specified.
    /// </summary>
    public string? LegalForm { get; init; }

    /// <summary>
    /// The industry of the company, or null if not specified.
    /// </summary>
    public string? Industry { get; init; }

    /// <summary>
    /// The company website URL.
    /// </summary>
    public string? Website { get; init; }

    /// <summary>
    /// The primary city of the company, or null if no location is available.
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// The number of employees, or null if not specified.
    /// </summary>
    public int? EmployeeCount { get; init; }
}
