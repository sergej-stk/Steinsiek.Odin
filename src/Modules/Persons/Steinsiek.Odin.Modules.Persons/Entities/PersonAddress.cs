namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a postal address associated with a person.
/// </summary>
public class PersonAddress : BaseEntity
{
    /// <summary>
    /// The identifier of the person this address belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The optional reference to an address type lookup entry.
    /// </summary>
    public Guid? AddressTypeId { get; set; }

    /// <summary>
    /// The primary street line of the address.
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// The optional secondary street line (e.g., apartment, suite).
    /// </summary>
    public string? Street2 { get; set; }

    /// <summary>
    /// The city name.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// The postal or ZIP code.
    /// </summary>
    public required string PostalCode { get; set; }

    /// <summary>
    /// The optional state or province.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// The reference to a country lookup entry.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Indicates whether this is the primary address for the person.
    /// </summary>
    public bool IsPrimary { get; set; }
}
