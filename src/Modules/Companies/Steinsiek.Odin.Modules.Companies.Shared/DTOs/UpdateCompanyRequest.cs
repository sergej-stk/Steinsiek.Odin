namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Request payload for updating an existing company.
/// </summary>
public sealed record class UpdateCompanyRequest
{
    /// <summary>
    /// The company name (required, max 200 characters).
    /// </summary>
    [Required, MaxLength(200)]
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
    [MaxLength(256)]
    public string? Website { get; init; }

    /// <summary>
    /// The company email address.
    /// </summary>
    [MaxLength(256), EmailAddress]
    public string? Email { get; init; }

    /// <summary>
    /// The company phone number.
    /// </summary>
    [MaxLength(256)]
    public string? Phone { get; init; }

    /// <summary>
    /// The company tax number.
    /// </summary>
    [MaxLength(100)]
    public string? TaxNumber { get; init; }

    /// <summary>
    /// The company VAT identification number.
    /// </summary>
    [MaxLength(100)]
    public string? VatId { get; init; }

    /// <summary>
    /// The commercial register number.
    /// </summary>
    [MaxLength(100)]
    public string? CommercialRegisterNumber { get; init; }

    /// <summary>
    /// The founding date of the company.
    /// </summary>
    public DateTime? FoundingDate { get; init; }

    /// <summary>
    /// The number of employees.
    /// </summary>
    public int? EmployeeCount { get; init; }

    /// <summary>
    /// The company revenue.
    /// </summary>
    public decimal? Revenue { get; init; }

    /// <summary>
    /// The parent company identifier for hierarchical relationships.
    /// </summary>
    public Guid? ParentCompanyId { get; init; }

    /// <summary>
    /// Additional notes about the company.
    /// </summary>
    [MaxLength(2000)]
    public string? Notes { get; init; }
}
