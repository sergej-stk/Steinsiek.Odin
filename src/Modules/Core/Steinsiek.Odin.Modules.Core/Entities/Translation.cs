namespace Steinsiek.Odin.Modules.Core.Entities;

/// <summary>
/// Represents a translated value for a lookup entity in a specific language.
/// Does not inherit from <see cref="BaseEntity"/> as translations are simple lookup records.
/// </summary>
public sealed class Translation
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the translated entity.
    /// </summary>
    public required Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the type name of the translated entity.
    /// </summary>
    public required string EntityType { get; set; }

    /// <summary>
    /// Gets or sets the language code for this translation.
    /// </summary>
    public required string LanguageCode { get; set; }

    /// <summary>
    /// Gets or sets the translated value.
    /// </summary>
    public required string Value { get; set; }
}
