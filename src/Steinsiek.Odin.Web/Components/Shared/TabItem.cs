namespace Steinsiek.Odin.Web.Components.Shared;

/// <summary>
/// Represents a single tab item in a <see cref="TabPanel"/> component.
/// </summary>
public sealed class TabItem
{
    /// <summary>
    /// Gets the tab title displayed in the tab header.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the optional Bootstrap icon class displayed before the title.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    /// Gets or sets the optional badge count displayed next to the title.
    /// </summary>
    public int Badge { get; set; }

    /// <summary>
    /// Gets the tab content rendered when the tab is active.
    /// </summary>
    public required RenderFragment Content { get; init; }
}
