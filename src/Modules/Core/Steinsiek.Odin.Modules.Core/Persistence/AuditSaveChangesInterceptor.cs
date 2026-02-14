namespace Steinsiek.Odin.Modules.Core.Persistence;

/// <summary>
/// EF Core save changes interceptor that records audit log entries for all changes to <see cref="BaseEntity"/> entities.
/// </summary>
public sealed class AuditSaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditSaveChangesInterceptor"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor for resolving the current user.</param>
    public AuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Collects audit log entries from the change tracker before saving.
    /// Must be called before <see cref="DbContext.SaveChangesAsync(CancellationToken)"/>.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>A list of audit log entries to persist.</returns>
    public List<AuditLogEntry> CollectAuditEntries(DbContext context)
    {
        var userId = GetCurrentUserId();
        var entries = new List<AuditLogEntry>();

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
            {
                continue;
            }

            var entityType = entry.Entity.GetType().Name;
            var entityId = entry.Entity.Id;

            switch (entry.State)
            {
                case EntityState.Added:
                    entries.Add(new AuditLogEntry
                    {
                        EntityType = entityType,
                        EntityId = entityId,
                        Action = "Created",
                        UserId = userId,
                        Timestamp = DateTime.UtcNow
                    });
                    break;

                case EntityState.Modified:
                    foreach (var prop in entry.Properties.Where(p => p.IsModified))
                    {
                        if (prop.Metadata.Name is nameof(BaseEntity.UpdatedAt)
                            or nameof(BaseEntity.CreatedAt))
                        {
                            continue;
                        }

                        entries.Add(new AuditLogEntry
                        {
                            EntityType = entityType,
                            EntityId = entityId,
                            Action = "Updated",
                            PropertyName = prop.Metadata.Name,
                            OldValue = prop.OriginalValue?.ToString(),
                            NewValue = prop.CurrentValue?.ToString(),
                            UserId = userId,
                            Timestamp = DateTime.UtcNow
                        });
                    }
                    break;

                case EntityState.Deleted:
                    entries.Add(new AuditLogEntry
                    {
                        EntityType = entityType,
                        EntityId = entityId,
                        Action = "Deleted",
                        UserId = userId,
                        Timestamp = DateTime.UtcNow
                    });
                    break;
            }
        }

        return entries;
    }

    /// <summary>
    /// Resolves the current user identifier from the JWT sub claim.
    /// </summary>
    private Guid? GetCurrentUserId()
    {
        var sub = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
        if (sub is not null && Guid.TryParse(sub, out var userId))
        {
            return userId;
        }

        return null;
    }
}
