namespace Steinsiek.Odin.Modules.Products;

/// <summary>
/// Module for product and category management functionality.
/// </summary>
public sealed class ProductsModule : IModule
{
    /// <inheritdoc />
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
    }
}
