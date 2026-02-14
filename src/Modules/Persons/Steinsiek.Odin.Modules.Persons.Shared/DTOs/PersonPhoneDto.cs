namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Data transfer object for a person's phone number.
/// </summary>
public sealed record class PersonPhoneDto
{
    /// <summary>
    /// The unique identifier of the phone number entry.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The phone number value.
    /// </summary>
    public required string Number { get; init; }

    /// <summary>
    /// The optional reference to a contact type lookup entry.
    /// </summary>
    public Guid? ContactTypeId { get; init; }

    /// <summary>
    /// An optional descriptive label.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Indicates whether this is the primary phone number.
    /// </summary>
    public bool IsPrimary { get; init; }
}
