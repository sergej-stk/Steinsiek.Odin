namespace Steinsiek.Odin.API.Services;

/// <summary>
/// Service for aggregating dashboard data across modules.
/// </summary>
public interface IDashboardService
{
    /// <summary>
    /// Retrieves aggregate statistics for the dashboard.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The dashboard statistics.</returns>
    Task<DashboardStatsDto> GetStats(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the most recent audit log entries for the dashboard.
    /// </summary>
    /// <param name="count">The maximum number of entries to return.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of audit log DTOs.</returns>
    Task<ListResult<AuditLogDto>> GetRecentActivity(int count, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves persons with upcoming birthdays within the specified number of days.
    /// </summary>
    /// <param name="days">The number of days to look ahead.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of person DTOs with upcoming birthdays.</returns>
    Task<ListResult<PersonDto>> GetUpcomingBirthdays(int days, CancellationToken cancellationToken);
}
