namespace Steinsiek.Odin.Modules.Core.Controllers;

/// <summary>
/// API controller for retrieving translated lookup data. All endpoints are publicly accessible.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class LookupsController(ITranslationService translationService, ILogger<LookupsController> logger)
    : ControllerBase, ILookupsController
{
    private readonly ITranslationService _translationService = translationService;
    private readonly ILogger<LookupsController> _logger = logger;

    /// <inheritdoc />
    [HttpGet("{type}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<LookupDto>>> GetByType(string type, [FromQuery] string? lang, CancellationToken cancellationToken)
    {
        var languageCode = lang ?? LanguageCodes.Default;

        _logger.LogInformation("Retrieving lookups for type {LookupType} in language {LanguageCode}", type, languageCode);

        var result = await _translationService.GetLookups(type, languageCode, cancellationToken);
        return Ok(result);
    }
}
