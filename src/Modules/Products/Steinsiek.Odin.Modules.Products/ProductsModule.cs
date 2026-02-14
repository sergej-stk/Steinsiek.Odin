namespace Steinsiek.Odin.Modules.Products;

/// <summary>
/// Module for product and category management functionality.
/// </summary>
public sealed class ProductsModule : IModule
{
    /// <inheritdoc />
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        services.AddScoped<IProductRepository, EfProductRepository>();
        services.AddScoped<IProductImageRepository, EfProductImageRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductImageService, ProductImageService>();
    }
}
