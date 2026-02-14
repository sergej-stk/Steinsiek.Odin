namespace Steinsiek.Odin.Web.Services;

/// <summary>
/// Client interface for accessing dashboard API endpoints.
/// </summary>
public interface IDashboardApiClient
{
    /// <summary>
    /// Retrieves aggregate dashboard statistics.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The dashboard statistics, or null if the request fails.</returns>
    Task<DashboardStatsDto?> GetStats(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the most recent audit log entries.
    /// </summary>
    /// <param name="count">The maximum number of entries to return.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of audit log entries, or null if the request fails.</returns>
    Task<ListResult<AuditLogDto>?> GetRecentActivity(int count, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves persons with upcoming birthdays.
    /// </summary>
    /// <param name="days">The number of days to look ahead.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of person DTOs, or null if the request fails.</returns>
    Task<ListResult<PersonDto>?> GetUpcomingBirthdays(int days, CancellationToken cancellationToken);
}
