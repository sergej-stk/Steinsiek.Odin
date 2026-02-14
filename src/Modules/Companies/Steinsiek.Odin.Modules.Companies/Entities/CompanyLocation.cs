namespace Steinsiek.Odin.Modules.Companies.Entities;

/// <summary>
/// Represents a physical location associated with a company.
/// </summary>
public class CompanyLocation : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the company this location belongs to.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the name of this location.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the primary street address.
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Gets or sets the secondary street address line.
    /// </summary>
    public string? Street2 { get; set; }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    public required string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the state or region name.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the country identifier.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Gets or sets the geographic latitude coordinate.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the geographic longitude coordinate.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the phone number for this location.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the email address for this location.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the primary location.
    /// </summary>
    public bool IsPrimary { get; set; }
}
