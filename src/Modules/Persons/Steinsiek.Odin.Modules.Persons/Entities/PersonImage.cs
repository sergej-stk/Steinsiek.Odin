namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents the profile image of a person, stored as binary data.
/// </summary>
public class PersonImage
{
    /// <summary>
    /// The identifier of the person this image belongs to. Acts as the primary key.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The MIME content type of the image (e.g., "image/png", "image/jpeg").
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// The original file name of the uploaded image.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// The raw binary data of the image.
    /// </summary>
    public required byte[] Data { get; set; }
}
