namespace Steinsiek.Odin.Modules.Persons;

/// <summary>
/// Module for person management functionality including CRUD operations and search.
/// </summary>
public sealed class PersonsModule : IModule
{
    /// <inheritdoc />
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, EfPersonRepository>();
        services.AddScoped<IPersonService, PersonService>();
    }
}
