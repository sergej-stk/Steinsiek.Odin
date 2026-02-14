namespace Steinsiek.Odin.Modules.Companies.Entities;

/// <summary>
/// Represents the image associated with a company. Uses CompanyId as the primary key.
/// </summary>
public class CompanyImage
{
    /// <summary>
    /// Gets or sets the company identifier, which also serves as the primary key.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the MIME content type of the image.
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the original file name of the image.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the raw image data.
    /// </summary>
    public required byte[] Data { get; set; }
}
