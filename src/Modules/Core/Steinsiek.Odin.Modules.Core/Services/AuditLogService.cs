namespace Steinsiek.Odin.Modules.Core.Services;

/// <summary>
/// Service implementation for querying audit log entries.
/// </summary>
public sealed class AuditLogService(IAuditLogRepository auditLogRepository) : IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository = auditLogRepository;

    /// <inheritdoc />
    public async Task<ListResult<AuditLogDto>> GetByEntity(string entityType, Guid entityId, CancellationToken cancellationToken)
    {
        var entries = await _auditLogRepository.GetByEntity(entityType, entityId, cancellationToken);
        var dtos = entries.Select(MapToDto).ToList();

        return new ListResult<AuditLogDto>
        {
            TotalCount = dtos.Count,
            Data = dtos
        };
    }

    /// <inheritdoc />
    public async Task<ListResult<AuditLogDto>> GetRecent(int count, CancellationToken cancellationToken)
    {
        var entries = await _auditLogRepository.GetRecent(count, cancellationToken);
        var dtos = entries.Select(MapToDto).ToList();

        return new ListResult<AuditLogDto>
        {
            TotalCount = dtos.Count,
            Data = dtos
        };
    }

    /// <summary>
    /// Maps an <see cref="AuditLogEntry"/> entity to an <see cref="AuditLogDto"/>.
    /// </summary>
    private static AuditLogDto MapToDto(AuditLogEntry entry)
    {
        return new AuditLogDto
        {
            Id = entry.Id,
            EntityType = entry.EntityType,
            EntityId = entry.EntityId,
            Action = entry.Action,
            PropertyName = entry.PropertyName,
            OldValue = entry.OldValue,
            NewValue = entry.NewValue,
            UserId = entry.UserId,
            Timestamp = entry.Timestamp
        };
    }
}
