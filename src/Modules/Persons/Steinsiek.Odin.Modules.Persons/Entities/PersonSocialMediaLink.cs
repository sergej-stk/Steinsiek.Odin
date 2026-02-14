namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a social media link associated with a person.
/// </summary>
public class PersonSocialMediaLink : BaseEntity
{
    /// <summary>
    /// The identifier of the person this social media link belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The social media platform name (e.g., "LinkedIn", "GitHub").
    /// </summary>
    public required string Platform { get; set; }

    /// <summary>
    /// The URL of the social media profile.
    /// </summary>
    public required string Url { get; set; }
}
