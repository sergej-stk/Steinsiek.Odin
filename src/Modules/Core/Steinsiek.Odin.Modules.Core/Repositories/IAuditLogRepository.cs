namespace Steinsiek.Odin.Modules.Core.Repositories;

/// <summary>
/// Repository for querying audit log entries.
/// </summary>
public interface IAuditLogRepository
{
    /// <summary>
    /// Retrieves audit log entries for a specific entity.
    /// </summary>
    /// <param name="entityType">The entity type name.</param>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of audit log entries.</returns>
    Task<IEnumerable<AuditLogEntry>> GetByEntity(string entityType, Guid entityId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the most recent audit log entries.
    /// </summary>
    /// <param name="count">The maximum number of entries to return.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of recent audit log entries.</returns>
    Task<IEnumerable<AuditLogEntry>> GetRecent(int count, CancellationToken cancellationToken);
}
