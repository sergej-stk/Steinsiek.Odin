namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a person in the system with personal details, contact information, and related sub-entities.
/// </summary>
public class Person : BaseEntity
{
    /// <summary>
    /// The optional reference to a salutation lookup entry.
    /// </summary>
    public Guid? SalutationId { get; set; }

    /// <summary>
    /// The optional academic or professional title (e.g., Dr., Prof.).
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The person's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// The person's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// The computed full name combining first and last name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// The optional date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// The optional reference to a gender lookup entry.
    /// </summary>
    public Guid? GenderId { get; set; }

    /// <summary>
    /// The optional reference to a nationality lookup entry.
    /// </summary>
    public Guid? NationalityId { get; set; }

    /// <summary>
    /// The optional reference to a marital status lookup entry.
    /// </summary>
    public Guid? MaritalStatusId { get; set; }

    /// <summary>
    /// Optional free-text notes about the person.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// The collection of postal addresses associated with the person.
    /// </summary>
    public ICollection<PersonAddress> Addresses { get; set; } = [];

    /// <summary>
    /// The collection of email addresses associated with the person.
    /// </summary>
    public ICollection<PersonEmailAddress> EmailAddresses { get; set; } = [];

    /// <summary>
    /// The collection of phone numbers associated with the person.
    /// </summary>
    public ICollection<PersonPhoneNumber> PhoneNumbers { get; set; } = [];

    /// <summary>
    /// The collection of bank accounts associated with the person.
    /// </summary>
    public ICollection<PersonBankAccount> BankAccounts { get; set; } = [];

    /// <summary>
    /// The collection of social media links associated with the person.
    /// </summary>
    public ICollection<PersonSocialMediaLink> SocialMediaLinks { get; set; } = [];

    /// <summary>
    /// The collection of languages spoken by the person.
    /// </summary>
    public ICollection<PersonLanguage> Languages { get; set; } = [];

    /// <summary>
    /// The optional profile image of the person.
    /// </summary>
    public PersonImage? Image { get; set; }
}
