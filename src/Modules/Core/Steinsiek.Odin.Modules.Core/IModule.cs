namespace Steinsiek.Odin.Modules.Core;

/// <summary>
/// Defines a module that can register its services with the dependency injection container.
/// </summary>
public interface IModule
{
    /// <summary>
    /// Registers the module's services with the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to register services with.</param>
    static abstract void RegisterServices(IServiceCollection services);
}
