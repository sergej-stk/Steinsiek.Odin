namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Request payload for creating a new person.
/// </summary>
public sealed record class CreatePersonRequest
{
    /// <summary>
    /// The optional reference to a salutation lookup entry.
    /// </summary>
    public Guid? SalutationId { get; init; }

    /// <summary>
    /// The optional academic or professional title.
    /// </summary>
    [MaxLength(50)]
    public string? Title { get; init; }

    /// <summary>
    /// The person's first name.
    /// </summary>
    [Required, MaxLength(100)]
    public required string FirstName { get; init; }

    /// <summary>
    /// The person's last name.
    /// </summary>
    [Required, MaxLength(100)]
    public required string LastName { get; init; }

    /// <summary>
    /// The optional date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; init; }

    /// <summary>
    /// The optional reference to a gender lookup entry.
    /// </summary>
    public Guid? GenderId { get; init; }

    /// <summary>
    /// The optional reference to a nationality lookup entry.
    /// </summary>
    public Guid? NationalityId { get; init; }

    /// <summary>
    /// The optional reference to a marital status lookup entry.
    /// </summary>
    public Guid? MaritalStatusId { get; init; }

    /// <summary>
    /// Optional free-text notes about the person.
    /// </summary>
    [MaxLength(2000)]
    public string? Notes { get; init; }
}
