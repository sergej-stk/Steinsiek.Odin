namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Detailed data transfer object for a company including locations and person associations.
/// </summary>
public sealed record class CompanyDetailDto
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
    /// The legal form identifier, or null if not specified.
    /// </summary>
    public Guid? LegalFormId { get; init; }

    /// <summary>
    /// The industry identifier, or null if not specified.
    /// </summary>
    public Guid? IndustryId { get; init; }

    /// <summary>
    /// The company website URL.
    /// </summary>
    public string? Website { get; init; }

    /// <summary>
    /// The company email address.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// The company phone number.
    /// </summary>
    public string? Phone { get; init; }

    /// <summary>
    /// The company tax number.
    /// </summary>
    public string? TaxNumber { get; init; }

    /// <summary>
    /// The company VAT identification number.
    /// </summary>
    public string? VatId { get; init; }

    /// <summary>
    /// The commercial register number.
    /// </summary>
    public string? CommercialRegisterNumber { get; init; }

    /// <summary>
    /// The founding date of the company.
    /// </summary>
    public DateTime? FoundingDate { get; init; }

    /// <summary>
    /// The number of employees, or null if not specified.
    /// </summary>
    public int? EmployeeCount { get; init; }

    /// <summary>
    /// The company revenue, or null if not specified.
    /// </summary>
    public decimal? Revenue { get; init; }

    /// <summary>
    /// The parent company identifier for hierarchical relationships.
    /// </summary>
    public Guid? ParentCompanyId { get; init; }

    /// <summary>
    /// Additional notes about the company.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// The collection of locations associated with this company.
    /// </summary>
    public required IReadOnlyList<CompanyLocationDto> Locations { get; init; }

    /// <summary>
    /// The collection of person-company associations.
    /// </summary>
    public required IReadOnlyList<PersonCompanyDto> PersonCompanies { get; init; }
}
