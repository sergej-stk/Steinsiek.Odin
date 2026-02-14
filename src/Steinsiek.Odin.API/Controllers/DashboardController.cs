namespace Steinsiek.Odin.API.Controllers;

/// <summary>
/// Controller providing dashboard aggregate data including statistics, recent activity, and upcoming birthdays.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public sealed class DashboardController(
    IDashboardService dashboardService,
    ILogger<DashboardController> logger) : ControllerBase, IDashboardController
{
    private readonly IDashboardService _dashboardService = dashboardService;
    private readonly ILogger<DashboardController> _logger = logger;

    /// <inheritdoc />
    [HttpGet("stats")]
    [ProducesResponseType(typeof(DashboardStatsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DashboardStatsDto>> GetStats(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Dashboard stats requested");

        var stats = await _dashboardService.GetStats(cancellationToken);
        return stats;
    }

    /// <inheritdoc />
    [HttpGet("recent-activity")]
    [ProducesResponseType(typeof(ListResult<AuditLogDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<AuditLogDto>>> GetRecentActivity(
        [FromQuery] int count = 10,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Dashboard recent activity requested (count={Count})", count);

        var result = await _dashboardService.GetRecentActivity(count, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    [HttpGet("upcoming-birthdays")]
    [ProducesResponseType(typeof(ListResult<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListResult<PersonDto>>> GetUpcomingBirthdays(
        [FromQuery] int days = 7,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Dashboard upcoming birthdays requested (days={Days})", days);

        var result = await _dashboardService.GetUpcomingBirthdays(days, cancellationToken);
        return result;
    }
}
