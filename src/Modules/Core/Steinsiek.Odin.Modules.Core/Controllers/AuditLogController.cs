namespace Steinsiek.Odin.Modules.Core.Controllers;

/// <summary>
/// API controller for querying audit log entries. Admin-only access.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/audit-log")]
[Authorize(Roles = OdinRoles.Admin)]
public sealed class AuditLogController(IAuditLogService auditLogService) : ControllerBase, IAuditLogController
{
    private readonly IAuditLogService _auditLogService = auditLogService;

    /// <inheritdoc />
    [HttpGet("recent")]
    [ProducesResponseType(typeof(ListResult<AuditLogDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<AuditLogDto>>> GetRecent([FromQuery] int count, CancellationToken cancellationToken)
    {
        if (count <= 0)
        {
            count = 10;
        }

        var result = await _auditLogService.GetRecent(count, cancellationToken);
        return Ok(result);
    }

    /// <inheritdoc />
    [HttpGet("{entityType}/{entityId:guid}")]
    [ProducesResponseType(typeof(ListResult<AuditLogDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<AuditLogDto>>> GetByEntity(string entityType, Guid entityId, CancellationToken cancellationToken)
    {
        var result = await _auditLogService.GetByEntity(entityType, entityId, cancellationToken);
        return Ok(result);
    }
}
