namespace Steinsiek.Odin.Modules.Persons.Controllers;

/// <summary>
/// API controller for person management operations.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class PersonsController(IPersonService personService, ILogger<PersonsController> logger)
    : ControllerBase, IPersonsController
{
    private readonly IPersonService _personService = personService;
    private readonly ILogger<PersonsController> _logger = logger;

    /// <inheritdoc />
    [HttpGet]
    [ProducesResponseType(typeof(ListResult<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<PersonDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _personService.GetAll(cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var person = await _personService.GetById(id, cancellationToken);
        if (person is null)
        {
            return NotFound();
        }

        return person;
    }

    /// <inheritdoc />
    [HttpGet("search")]
    [ProducesResponseType(typeof(ListResult<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<PersonDto>>> Search([FromQuery] string? q, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            var all = await _personService.GetAll(cancellationToken);
            return Ok(all);
        }

        var result = await _personService.Search(q, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var created = await _personService.Create(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    /// <inheritdoc />
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonDto>> Update(Guid id, [FromBody] UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        var updated = await _personService.Update(id, request, cancellationToken);
        if (updated is null)
        {
            return NotFound();
        }

        return updated;
    }

    /// <inheritdoc />
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _personService.Delete(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
