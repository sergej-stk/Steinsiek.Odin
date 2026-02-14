namespace Steinsiek.Odin.Modules.Core.Repositories;

/// <summary>
/// EF Core implementation of <see cref="IAuditLogRepository"/>.
/// </summary>
public sealed class EfAuditLogRepository(OdinDbContext context) : IAuditLogRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLogEntry>> GetByEntity(string entityType, Guid entityId, CancellationToken cancellationToken)
    {
        return await _context.Set<AuditLogEntry>()
            .Where(e => e.EntityType == entityType && e.EntityId == entityId)
            .OrderByDescending(e => e.Timestamp)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLogEntry>> GetRecent(int count, CancellationToken cancellationToken)
    {
        return await _context.Set<AuditLogEntry>()
            .OrderByDescending(e => e.Timestamp)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
