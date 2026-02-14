namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Data transfer object for a person's email address.
/// </summary>
public sealed record class PersonEmailDto
{
    /// <summary>
    /// The unique identifier of the email address entry.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The email address value.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// An optional descriptive label.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Indicates whether this is the primary email address.
    /// </summary>
    public bool IsPrimary { get; init; }
}
