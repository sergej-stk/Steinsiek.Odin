namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Data transfer object for a person-company association.
/// </summary>
public sealed record class PersonCompanyDto
{
    /// <summary>
    /// The unique identifier of the association.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The person identifier.
    /// </summary>
    public required Guid PersonId { get; init; }

    /// <summary>
    /// The company identifier.
    /// </summary>
    public required Guid CompanyId { get; init; }

    /// <summary>
    /// The position identifier, or null if not specified.
    /// </summary>
    public Guid? PositionId { get; init; }

    /// <summary>
    /// The department identifier, or null if not specified.
    /// </summary>
    public Guid? DepartmentId { get; init; }

    /// <summary>
    /// The start date of the association.
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// The end date of the association, or null if still active.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Whether this person-company association is currently active.
    /// </summary>
    public bool IsActive { get; init; }
}
