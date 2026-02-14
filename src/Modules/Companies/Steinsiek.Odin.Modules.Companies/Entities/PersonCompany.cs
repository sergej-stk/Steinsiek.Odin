namespace Steinsiek.Odin.Modules.Companies.Entities;

/// <summary>
/// Junction entity representing the association between a person and a company, including role and tenure details.
/// </summary>
public class PersonCompany : BaseEntity
{
    /// <summary>
    /// Gets or sets the person identifier. This is a cross-module reference without navigation property.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Gets or sets the company identifier.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the position identifier, or null if not specified.
    /// </summary>
    public Guid? PositionId { get; set; }

    /// <summary>
    /// Gets or sets the department identifier, or null if not specified.
    /// </summary>
    public Guid? DepartmentId { get; set; }

    /// <summary>
    /// Gets or sets the start date of the association.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the association, or null if still active.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this person-company association is currently active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the associated company, or null if not loaded.
    /// </summary>
    public Company? Company { get; set; }
}
