namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Client interface for lookup API operations.
/// </summary>
public interface ILookupApiClient
{
    /// <summary>
    /// Retrieves lookup entries for a given type.
    /// </summary>
    /// <param name="type">The lookup type (e.g. "salutations", "genders").</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The lookup entries, or an empty list if the request failed.</returns>
    Task<IReadOnlyList<LookupDto>> GetByType(string type, CancellationToken cancellationToken);
}
