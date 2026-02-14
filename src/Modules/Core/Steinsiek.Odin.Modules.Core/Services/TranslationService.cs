namespace Steinsiek.Odin.Modules.Core.Services;

/// <summary>
/// Service implementation for retrieving translated lookup data from the database.
/// </summary>
public sealed class TranslationService(OdinDbContext context, ILogger<TranslationService> logger) : ITranslationService
{
    private readonly OdinDbContext _context = context;
    private readonly ILogger<TranslationService> _logger = logger;

    /// <summary>
    /// Maps lowercase lookup type names to their corresponding entity type names used in translations.
    /// </summary>
    private static readonly Dictionary<string, string> LookupTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["salutation"] = "Salutation",
        ["gender"] = "Gender",
        ["maritalstatus"] = "MaritalStatus",
        ["country"] = "Country",
        ["addresstype"] = "AddressType",
        ["contacttype"] = "ContactType",
        ["industry"] = "Industry",
        ["legalform"] = "LegalForm",
        ["department"] = "Department",
        ["position"] = "Position"
    };

    /// <inheritdoc />
    public async Task<ListResult<LookupDto>> GetLookups(string lookupType, string languageCode, CancellationToken cancellationToken)
    {
        if (!LookupTypeMap.TryGetValue(lookupType, out var entityTypeName))
        {
            _logger.LogWarning("Unknown lookup type requested: {LookupType}", lookupType);

            return new ListResult<LookupDto>
            {
                TotalCount = 0,
                Data = []
            };
        }

        var lookups = await GetLookupEntries(entityTypeName, languageCode, cancellationToken);

        return new ListResult<LookupDto>
        {
            TotalCount = lookups.Count,
            Data = lookups
        };
    }

    /// <summary>
    /// Queries the lookup entities and joins them with translations for the specified language.
    /// </summary>
    /// <param name="entityTypeName">The entity type name as stored in translations.</param>
    /// <param name="languageCode">The ISO language code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of translated lookup DTOs ordered by sort order.</returns>
    private async Task<List<LookupDto>> GetLookupEntries(string entityTypeName, string languageCode, CancellationToken cancellationToken)
    {
        var translations = await _context.Set<Translation>()
            .Where(t => t.EntityType == entityTypeName && t.LanguageCode == languageCode)
            .ToListAsync(cancellationToken);

        var translationMap = translations.ToDictionary(t => t.EntityId, t => t.Value);

        return entityTypeName switch
        {
            "Salutation" => await BuildLookupDtos<Salutation>(translationMap, cancellationToken),
            "Gender" => await BuildLookupDtos<Gender>(translationMap, cancellationToken),
            "MaritalStatus" => await BuildLookupDtos<MaritalStatus>(translationMap, cancellationToken),
            "Country" => await BuildLookupDtos<Country>(translationMap, cancellationToken),
            "AddressType" => await BuildLookupDtos<AddressType>(translationMap, cancellationToken),
            "ContactType" => await BuildLookupDtos<ContactType>(translationMap, cancellationToken),
            "Industry" => await BuildLookupDtos<Industry>(translationMap, cancellationToken),
            "LegalForm" => await BuildLookupDtos<LegalForm>(translationMap, cancellationToken),
            "Department" => await BuildLookupDtos<Department>(translationMap, cancellationToken),
            "Position" => await BuildLookupDtos<Position>(translationMap, cancellationToken),
            _ => []
        };
    }

    /// <summary>
    /// Builds lookup DTOs by querying entities of the specified type and mapping translations.
    /// </summary>
    /// <typeparam name="T">The translatable entity type.</typeparam>
    /// <param name="translationMap">A dictionary mapping entity IDs to translated values.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of lookup DTOs ordered by sort order.</returns>
    private async Task<List<LookupDto>> BuildLookupDtos<T>(Dictionary<Guid, string> translationMap, CancellationToken cancellationToken) where T : TranslatableEntity
    {
        var entities = await _context.Set<T>()
            .OrderBy(e => e.SortOrder)
            .ToListAsync(cancellationToken);

        return entities
            .Select(e => new LookupDto
            {
                Id = e.Id,
                Code = e.Code,
                Value = translationMap.TryGetValue(e.Id, out var value) ? value : e.Code,
                SortOrder = e.SortOrder
            })
            .ToList();
    }
}
