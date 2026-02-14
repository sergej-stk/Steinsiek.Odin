namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a phone number associated with a person.
/// </summary>
public class PersonPhoneNumber : BaseEntity
{
    /// <summary>
    /// The identifier of the person this phone number belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The phone number value.
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// The optional reference to a contact type lookup entry.
    /// </summary>
    public Guid? ContactTypeId { get; set; }

    /// <summary>
    /// An optional descriptive label (e.g., "Mobile", "Office").
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Indicates whether this is the primary phone number for the person.
    /// </summary>
    public bool IsPrimary { get; set; }
}
