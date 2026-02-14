namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a language spoken by a person with an optional proficiency level.
/// </summary>
public class PersonLanguage : BaseEntity
{
    /// <summary>
    /// The identifier of the person this language entry belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The reference to a language lookup entry.
    /// </summary>
    public Guid LanguageId { get; set; }

    /// <summary>
    /// The optional proficiency level (e.g., "Native", "Fluent", "Intermediate").
    /// </summary>
    public string? ProficiencyLevel { get; set; }
}
