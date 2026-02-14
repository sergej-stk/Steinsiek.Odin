namespace Steinsiek.Odin.Modules.Companies.Controllers;

/// <summary>
/// API controller for company management operations.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
    : ControllerBase, ICompaniesController
{
    private readonly ICompanyService _companyService = companyService;
    private readonly ILogger<CompaniesController> _logger = logger;

    /// <inheritdoc />
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<CompanyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _companyService.GetAll(cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("paged")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PagedResult<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<CompanyDto>>> GetPaged([FromQuery] PagedQuery query, [FromQuery] CompanyFilterQuery filter, CancellationToken cancellationToken)
    {
        var result = await _companyService.GetPaged(query, filter, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CompanyDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _companyService.GetById(id, cancellationToken);
        if (result is null)
        {
            return NotFound();
        }

        return result;
    }

    /// <inheritdoc />
    [HttpGet("search")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ListResult<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ListResult<CompanyDto>>> Search([FromQuery] string? q, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return Ok(new ListResult<CompanyDto>
            {
                TotalCount = 0,
                Data = []
            });
        }

        var result = await _companyService.Search(q, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CompanyDto>> Create([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var result = await _companyService.Create(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <inheritdoc />
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> Update(Guid id, [FromBody] UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var result = await _companyService.Update(id, request, cancellationToken);
        if (result is null)
        {
            return NotFound();
        }

        return result;
    }

    /// <inheritdoc />
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _companyService.Delete(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <inheritdoc />
    [HttpDelete("bulk")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> DeleteMany([FromBody] IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
    {
        var deletedCount = await _companyService.DeleteMany(ids, cancellationToken);
        return deletedCount;
    }
}
