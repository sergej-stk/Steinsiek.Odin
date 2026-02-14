namespace Steinsiek.Odin.API.Controllers;

/// <summary>
/// Interface for the dashboard controller providing aggregate statistics and recent activity.
/// </summary>
public interface IDashboardController
{
    /// <summary>
    /// Retrieves aggregate statistics for the dashboard.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The dashboard statistics.</returns>
    Task<ActionResult<DashboardStatsDto>> GetStats(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the most recent audit log entries.
    /// </summary>
    /// <param name="count">The maximum number of entries to return.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of audit log entries.</returns>
    Task<ActionResult<ListResult<AuditLogDto>>> GetRecentActivity(int count, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves persons with upcoming birthdays.
    /// </summary>
    /// <param name="days">The number of days to look ahead.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of person DTOs.</returns>
    Task<ActionResult<ListResult<PersonDto>>> GetUpcomingBirthdays(int days, CancellationToken cancellationToken);
}
