namespace Steinsiek.Odin.Modules.Core.Entities;

/// <summary>
/// Abstract base class for lookup entities that support translations.
/// </summary>
public abstract class TranslatableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier code for this lookup entry.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Gets or sets the sort order for display purposes.
    /// </summary>
    public int SortOrder { get; set; }
}
