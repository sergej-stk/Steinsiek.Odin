namespace Steinsiek.Odin.Modules.Core.Persistence;

/// <summary>
/// Central database context for the Odin application, applying entity configurations from all registered module assemblies.
/// Provides global soft-delete query filters, automatic soft-delete interception, and audit logging on save.
/// </summary>
public sealed class OdinDbContext : DbContext
{
    private readonly OdinDbContextOptions _moduleOptions;
    private readonly AuditSaveChangesInterceptor? _auditInterceptor;

    /// <summary>
    /// Initializes a new instance of the <see cref="OdinDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    /// <param name="moduleOptions">The module assembly options for entity configuration discovery.</param>
    /// <param name="auditInterceptor">The audit interceptor for recording changes.</param>
    public OdinDbContext(
        DbContextOptions<OdinDbContext> options,
        IOptions<OdinDbContextOptions> moduleOptions,
        AuditSaveChangesInterceptor? auditInterceptor = null)
        : base(options)
    {
        _moduleOptions = moduleOptions.Value;
        _auditInterceptor = auditInterceptor;
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var assembly in _moduleOptions.ModuleAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        ApplySoftDeleteFilters(modelBuilder);
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        InterceptSoftDelete();
        var auditEntries = _auditInterceptor?.CollectAuditEntries(this);
        var result = base.SaveChanges();
        PersistAuditEntries(auditEntries);
        return result;
    }

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        InterceptSoftDelete();
        var auditEntries = _auditInterceptor?.CollectAuditEntries(this);
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        PersistAuditEntries(auditEntries);
        return result;
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        InterceptSoftDelete();
        var auditEntries = _auditInterceptor?.CollectAuditEntries(this);
        var result = await base.SaveChangesAsync(cancellationToken);
        await PersistAuditEntriesAsync(auditEntries, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        InterceptSoftDelete();
        var auditEntries = _auditInterceptor?.CollectAuditEntries(this);
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await PersistAuditEntriesAsync(auditEntries, cancellationToken);
        return result;
    }

    /// <summary>
    /// Applies global query filters for soft-delete on all entity types deriving from <see cref="BaseEntity"/>.
    /// </summary>
    private static void ApplySoftDeleteFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
            var condition = Expression.Equal(property, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }

    /// <summary>
    /// Intercepts entities marked for deletion and converts them to soft-delete updates.
    /// </summary>
    private void InterceptSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()
                     .Where(e => e.State == EntityState.Deleted))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Persists collected audit log entries synchronously.
    /// </summary>
    private void PersistAuditEntries(List<AuditLogEntry>? entries)
    {
        if (entries is null || entries.Count == 0)
        {
            return;
        }

        Set<AuditLogEntry>().AddRange(entries);
        base.SaveChanges();
    }

    /// <summary>
    /// Persists collected audit log entries asynchronously.
    /// </summary>
    private async Task PersistAuditEntriesAsync(List<AuditLogEntry>? entries, CancellationToken cancellationToken)
    {
        if (entries is null || entries.Count == 0)
        {
            return;
        }

        Set<AuditLogEntry>().AddRange(entries);
        await base.SaveChangesAsync(cancellationToken);
    }
}
