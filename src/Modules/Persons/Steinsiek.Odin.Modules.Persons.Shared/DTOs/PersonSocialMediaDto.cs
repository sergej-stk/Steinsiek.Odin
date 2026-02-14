namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Data transfer object for a person's social media link.
/// </summary>
public sealed record class PersonSocialMediaDto
{
    /// <summary>
    /// The unique identifier of the social media link entry.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The social media platform name.
    /// </summary>
    public required string Platform { get; init; }

    /// <summary>
    /// The URL of the social media profile.
    /// </summary>
    public required string Url { get; init; }
}
