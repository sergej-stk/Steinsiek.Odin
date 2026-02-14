namespace Steinsiek.Odin.Modules.Core.Entities;

/// <summary>
/// Represents a supported language in the system.
/// </summary>
public sealed class Language : BaseEntity
{
    /// <summary>
    /// Gets or sets the ISO language code (e.g. "de", "en").
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the display name of the language.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the default language.
    /// </summary>
    public bool IsDefault { get; set; }
}
