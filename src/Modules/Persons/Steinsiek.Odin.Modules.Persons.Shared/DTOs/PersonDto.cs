namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Summary data transfer object for a person, used in list views.
/// </summary>
public sealed record class PersonDto
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The person's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The person's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The optional academic or professional title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The primary email address, if available.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// The primary phone number, if available.
    /// </summary>
    public string? Phone { get; init; }

    /// <summary>
    /// The city from the primary address, if available.
    /// </summary>
    public string? City { get; init; }
}
