namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Filter parameters for person list queries.
/// </summary>
public sealed record class PersonFilterQuery
{
    /// <summary>
    /// Filters by city (partial match on primary address).
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Filters by salutation lookup identifier.
    /// </summary>
    public Guid? SalutationId { get; init; }

    /// <summary>
    /// Filters by gender lookup identifier.
    /// </summary>
    public Guid? GenderId { get; init; }

    /// <summary>
    /// Filters by nationality lookup identifier.
    /// </summary>
    public Guid? NationalityId { get; init; }

    /// <summary>
    /// Filters by marital status lookup identifier.
    /// </summary>
    public Guid? MaritalStatusId { get; init; }

    /// <summary>
    /// Filters persons created on or after this date.
    /// </summary>
    public DateTime? CreatedFrom { get; init; }

    /// <summary>
    /// Filters persons created on or before this date.
    /// </summary>
    public DateTime? CreatedTo { get; init; }
}
