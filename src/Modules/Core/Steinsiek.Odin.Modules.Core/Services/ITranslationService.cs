namespace Steinsiek.Odin.Modules.Core.Services;

/// <summary>
/// Service for retrieving translated lookup data.
/// </summary>
public interface ITranslationService
{
    /// <summary>
    /// Retrieves all entries for the specified lookup type, translated to the given language.
    /// </summary>
    /// <param name="lookupType">The lookup type name (e.g. "salutation", "gender").</param>
    /// <param name="languageCode">The ISO language code (e.g. "de", "en").</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of translated lookup DTOs.</returns>
    Task<ListResult<LookupDto>> GetLookups(string lookupType, string languageCode, CancellationToken cancellationToken);
}
