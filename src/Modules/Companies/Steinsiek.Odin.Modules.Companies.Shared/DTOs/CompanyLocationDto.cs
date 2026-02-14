namespace Steinsiek.Odin.Modules.Companies.Shared.DTOs;

/// <summary>
/// Data transfer object for a company location.
/// </summary>
public sealed record class CompanyLocationDto
{
    /// <summary>
    /// The unique identifier of the location.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The name of the location.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The primary street address.
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// The secondary street address line.
    /// </summary>
    public string? Street2 { get; init; }

    /// <summary>
    /// The city name.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// The postal code.
    /// </summary>
    public required string PostalCode { get; init; }

    /// <summary>
    /// The state or region name.
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// The country identifier.
    /// </summary>
    public required Guid CountryId { get; init; }

    /// <summary>
    /// The geographic latitude coordinate.
    /// </summary>
    public double? Latitude { get; init; }

    /// <summary>
    /// The geographic longitude coordinate.
    /// </summary>
    public double? Longitude { get; init; }

    /// <summary>
    /// The phone number for this location.
    /// </summary>
    public string? Phone { get; init; }

    /// <summary>
    /// The email address for this location.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Whether this is the primary location.
    /// </summary>
    public bool IsPrimary { get; init; }
}
