namespace Steinsiek.Odin.Modules.Core.Controllers;

/// <summary>
/// Interface for the lookups API controller.
/// </summary>
public interface ILookupsController
{
    /// <summary>
    /// Retrieves all entries for a lookup type, translated to the specified language.
    /// </summary>
    /// <param name="type">The lookup type name (e.g. "salutation", "gender").</param>
    /// <param name="lang">The ISO language code (e.g. "de", "en").</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of translated lookup DTOs.</returns>
    Task<ActionResult<ListResult<LookupDto>>> GetByType(string type, string? lang, CancellationToken cancellationToken);
}
