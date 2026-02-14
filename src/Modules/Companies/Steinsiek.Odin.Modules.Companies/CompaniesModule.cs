namespace Steinsiek.Odin.Modules.Companies;

/// <summary>
/// Module for company management functionality including CRUD operations and search.
/// </summary>
public sealed class CompaniesModule : IModule
{
    /// <inheritdoc />
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, EfCompanyRepository>();
        services.AddScoped<ICompanyService, CompanyService>();
    }
}
