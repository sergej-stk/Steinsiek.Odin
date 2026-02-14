namespace Steinsiek.Odin.Modules.Core.Services;

/// <summary>
/// Service for querying audit log entries.
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    /// Retrieves audit log entries for a specific entity.
    /// </summary>
    /// <param name="entityType">The entity type name.</param>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of audit log DTOs.</returns>
    Task<ListResult<AuditLogDto>> GetByEntity(string entityType, Guid entityId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the most recent audit log entries.
    /// </summary>
    /// <param name="count">The maximum number of entries to return.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list result of audit log DTOs.</returns>
    Task<ListResult<AuditLogDto>> GetRecent(int count, CancellationToken cancellationToken);
}
