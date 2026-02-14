namespace Steinsiek.Odin.Modules.Companies.Entities;

/// <summary>
/// Represents a company in the system.
/// </summary>
public class Company : BaseEntity
{
    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the legal form identifier, or null if not specified.
    /// </summary>
    public Guid? LegalFormId { get; set; }

    /// <summary>
    /// Gets or sets the industry identifier, or null if not specified.
    /// </summary>
    public Guid? IndustryId { get; set; }

    /// <summary>
    /// Gets or sets the company website URL.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the company email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the company phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the company tax number.
    /// </summary>
    public string? TaxNumber { get; set; }

    /// <summary>
    /// Gets or sets the company VAT identification number.
    /// </summary>
    public string? VatId { get; set; }

    /// <summary>
    /// Gets or sets the commercial register number.
    /// </summary>
    public string? CommercialRegisterNumber { get; set; }

    /// <summary>
    /// Gets or sets the founding date of the company.
    /// </summary>
    public DateTime? FoundingDate { get; set; }

    /// <summary>
    /// Gets or sets the number of employees.
    /// </summary>
    public int? EmployeeCount { get; set; }

    /// <summary>
    /// Gets or sets the company revenue.
    /// </summary>
    public decimal? Revenue { get; set; }

    /// <summary>
    /// Gets or sets the parent company identifier for hierarchical relationships.
    /// </summary>
    public Guid? ParentCompanyId { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the company.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the collection of locations associated with this company.
    /// </summary>
    public ICollection<CompanyLocation> Locations { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of person-company associations.
    /// </summary>
    public ICollection<PersonCompany> PersonCompanies { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of child companies.
    /// </summary>
    public ICollection<Company> ChildCompanies { get; set; } = [];

    /// <summary>
    /// Gets or sets the parent company, or null if this is a top-level company.
    /// </summary>
    public Company? ParentCompany { get; set; }

    /// <summary>
    /// Gets or sets the company image, or null if no image is assigned.
    /// </summary>
    public CompanyImage? Image { get; set; }
}
