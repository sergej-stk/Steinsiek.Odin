namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Detailed data transfer object for a person, including all sub-entity collections.
/// </summary>
public sealed record class PersonDetailDto
{
    /// <summary>
    /// The unique identifier of the person.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The optional reference to a salutation lookup entry.
    /// </summary>
    public Guid? SalutationId { get; init; }

    /// <summary>
    /// The optional academic or professional title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The person's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The person's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// The computed full name combining first and last name.
    /// </summary>
    public required string FullName { get; init; }

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
    public string? Notes { get; init; }

    /// <summary>
    /// The creation timestamp.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// The last update timestamp, or null if never updated.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>
    /// The collection of postal addresses.
    /// </summary>
    public IReadOnlyList<PersonAddressDto> Addresses { get; init; } = [];

    /// <summary>
    /// The collection of email addresses.
    /// </summary>
    public IReadOnlyList<PersonEmailDto> EmailAddresses { get; init; } = [];

    /// <summary>
    /// The collection of phone numbers.
    /// </summary>
    public IReadOnlyList<PersonPhoneDto> PhoneNumbers { get; init; } = [];

    /// <summary>
    /// The collection of bank accounts.
    /// </summary>
    public IReadOnlyList<PersonBankAccountDto> BankAccounts { get; init; } = [];

    /// <summary>
    /// The collection of social media links.
    /// </summary>
    public IReadOnlyList<PersonSocialMediaDto> SocialMediaLinks { get; init; } = [];
}
