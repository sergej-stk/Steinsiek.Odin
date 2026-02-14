namespace Steinsiek.Odin.Modules.Products.Persistence;

/// <summary>
/// Seeds product images from embedded resources into the database at application startup.
/// </summary>
public static class ProductImageSeeder
{
    private static readonly Dictionary<Guid, string> SeedImages = new()
    {
        { Guid.Parse("11111111-0001-0001-0001-000000000001"), "iphone-15-pro.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000002"), "macbook-air-m3.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000003"), "galaxy-s24-ultra.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000004"), "premium-hoodie.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000005"), "designer-jeans.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000006"), "clean-code.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000007"), "design-patterns.jpg" },
        { Guid.Parse("11111111-0001-0001-0001-000000000008"), "coffee-machine-deluxe.jpg" }
    };

    /// <summary>
    /// Seeds product images from embedded resources if they do not already exist in the database.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public static void Seed(OdinDbContext dbContext)
    {
        var imageSet = dbContext.Set<ProductImageData>();
        var assembly = typeof(ProductImageSeeder).Assembly;

        foreach (var (productId, fileName) in SeedImages)
        {
            if (imageSet.Any(i => i.ProductId == productId))
            {
                continue;
            }

            var resourceName = $"Steinsiek.Odin.Modules.Products.SeedImages.{fileName.Replace('-', '_')}";
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                continue;
            }

            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            imageSet.Add(new ProductImageData
            {
                ProductId = productId,
                ContentType = "image/jpeg",
                FileName = fileName,
                Data = memoryStream.ToArray()
            });
        }

        dbContext.SaveChanges();
    }
}
