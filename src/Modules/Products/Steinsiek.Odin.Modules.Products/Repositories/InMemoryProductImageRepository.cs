namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// In-memory implementation of the product image repository for development and testing.
/// </summary>
public sealed class InMemoryProductImageRepository : IProductImageRepository
{
    private readonly ConcurrentDictionary<Guid, ProductImageData> _images = new();

    /// <summary>
    /// Initializes a new instance and seeds demo image data from embedded resources.
    /// </summary>
    public InMemoryProductImageRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var seedImages = new Dictionary<Guid, string>
        {
            { InMemoryProductRepository.IPhone15ProId, "iphone-15-pro.jpg" },
            { InMemoryProductRepository.MacBookAirM3Id, "macbook-air-m3.jpg" },
            { InMemoryProductRepository.GalaxyS24UltraId, "galaxy-s24-ultra.jpg" },
            { InMemoryProductRepository.PremiumHoodieId, "premium-hoodie.jpg" },
            { InMemoryProductRepository.DesignerJeansId, "designer-jeans.jpg" },
            { InMemoryProductRepository.CleanCodeId, "clean-code.jpg" },
            { InMemoryProductRepository.DesignPatternsId, "design-patterns.jpg" },
            { InMemoryProductRepository.CoffeeMachineDeluxeId, "coffee-machine-deluxe.jpg" }
        };

        var assembly = Assembly.GetExecutingAssembly();

        foreach (var (productId, fileName) in seedImages)
        {
            var resourceName = $"Steinsiek.Odin.Modules.Products.SeedImages.{fileName.Replace('-', '_')}";
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                continue;
            }

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            _images.TryAdd(productId, new ProductImageData
            {
                ProductId = productId,
                ContentType = "image/jpeg",
                FileName = fileName,
                Data = memoryStream.ToArray()
            });
        }
    }

    /// <inheritdoc />
    public Task<ProductImageData?> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        _images.TryGetValue(productId, out var imageData);
        return Task.FromResult(imageData);
    }

    /// <inheritdoc />
    public Task Upsert(ProductImageData imageData, CancellationToken cancellationToken)
    {
        _images[imageData.ProductId] = imageData;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<bool> Delete(Guid productId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_images.TryRemove(productId, out _));
    }

    /// <inheritdoc />
    public Task<bool> Exists(Guid productId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_images.ContainsKey(productId));
    }
}
