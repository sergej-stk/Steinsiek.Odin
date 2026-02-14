namespace Steinsiek.Odin.API;

/// <summary>
/// Design-time factory for creating <see cref="OdinDbContext"/> instances used by EF Core CLI tools for migrations.
/// </summary>
public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OdinDbContext>
{
    /// <inheritdoc />
    public OdinDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OdinDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=odindb;Username=postgres;Password=postgres");

        var moduleOptions = Microsoft.Extensions.Options.Options.Create(new OdinDbContextOptions());
        moduleOptions.Value.ModuleAssemblies.Add(typeof(Steinsiek.Odin.Modules.Auth.AuthModule).Assembly);
        moduleOptions.Value.ModuleAssemblies.Add(typeof(Steinsiek.Odin.Modules.Products.ProductsModule).Assembly);

        return new OdinDbContext(optionsBuilder.Options, moduleOptions);
    }
}
