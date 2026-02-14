namespace Steinsiek.Odin.Modules.Core.Persistence;

/// <summary>
/// Configuration options for the <see cref="OdinDbContext"/> specifying which module assemblies to scan for entity configurations.
/// </summary>
public sealed class OdinDbContextOptions
{
    /// <summary>
    /// Gets the list of module assemblies containing <see cref="IEntityTypeConfiguration{TEntity}"/> implementations.
    /// </summary>
    public List<Assembly> ModuleAssemblies { get; } = [];
}
