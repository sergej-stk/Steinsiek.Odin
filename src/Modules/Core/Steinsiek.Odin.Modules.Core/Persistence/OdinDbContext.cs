namespace Steinsiek.Odin.Modules.Core.Persistence;

/// <summary>
/// Central database context for the Odin application, applying entity configurations from all registered module assemblies.
/// </summary>
public sealed class OdinDbContext : DbContext
{
    private readonly OdinDbContextOptions _moduleOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="OdinDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    /// <param name="moduleOptions">The module assembly options for entity configuration discovery.</param>
    public OdinDbContext(DbContextOptions<OdinDbContext> options, IOptions<OdinDbContextOptions> moduleOptions)
        : base(options)
    {
        _moduleOptions = moduleOptions.Value;
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var assembly in _moduleOptions.ModuleAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
