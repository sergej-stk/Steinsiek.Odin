namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents an email address associated with a person.
/// </summary>
public class PersonEmailAddress : BaseEntity
{
    /// <summary>
    /// The identifier of the person this email address belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The email address value.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// An optional descriptive label (e.g., "Work", "Personal").
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Indicates whether this is the primary email address for the person.
    /// </summary>
    public bool IsPrimary { get; set; }
}
