namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Data transfer object for a person's postal address.
/// </summary>
public sealed record class PersonAddressDto
{
    /// <summary>
    /// The unique identifier of the address.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The optional reference to an address type lookup entry.
    /// </summary>
    public Guid? AddressTypeId { get; init; }

    /// <summary>
    /// The primary street line of the address.
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// The optional secondary street line.
    /// </summary>
    public string? Street2 { get; init; }

    /// <summary>
    /// The city name.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// The postal or ZIP code.
    /// </summary>
    public required string PostalCode { get; init; }

    /// <summary>
    /// The optional state or province.
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// The reference to a country lookup entry.
    /// </summary>
    public required Guid CountryId { get; init; }

    /// <summary>
    /// Indicates whether this is the primary address.
    /// </summary>
    public bool IsPrimary { get; init; }
}
