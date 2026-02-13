namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// In-memory implementation of the product repository for development and testing.
/// </summary>
public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();

    /// <summary>
    /// Predefined identifier for the iPhone 15 Pro product.
    /// </summary>
    public static readonly Guid IPhone15ProId = Guid.Parse("11111111-0001-0001-0001-000000000001");

    /// <summary>
    /// Predefined identifier for the MacBook Air M3 product.
    /// </summary>
    public static readonly Guid MacBookAirM3Id = Guid.Parse("11111111-0001-0001-0001-000000000002");

    /// <summary>
    /// Predefined identifier for the Samsung Galaxy S24 Ultra product.
    /// </summary>
    public static readonly Guid GalaxyS24UltraId = Guid.Parse("11111111-0001-0001-0001-000000000003");

    /// <summary>
    /// Predefined identifier for the Premium Hoodie product.
    /// </summary>
    public static readonly Guid PremiumHoodieId = Guid.Parse("11111111-0001-0001-0001-000000000004");

    /// <summary>
    /// Predefined identifier for the Designer Jeans product.
    /// </summary>
    public static readonly Guid DesignerJeansId = Guid.Parse("11111111-0001-0001-0001-000000000005");

    /// <summary>
    /// Predefined identifier for the Clean Code book product.
    /// </summary>
    public static readonly Guid CleanCodeId = Guid.Parse("11111111-0001-0001-0001-000000000006");

    /// <summary>
    /// Predefined identifier for the Design Patterns book product.
    /// </summary>
    public static readonly Guid DesignPatternsId = Guid.Parse("11111111-0001-0001-0001-000000000007");

    /// <summary>
    /// Predefined identifier for the Coffee Machine Deluxe product.
    /// </summary>
    public static readonly Guid CoffeeMachineDeluxeId = Guid.Parse("11111111-0001-0001-0001-000000000008");

    /// <summary>
    /// Initializes a new instance and seeds demo data.
    /// </summary>
    public InMemoryProductRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var products = new[]
        {
            new Product
            {
                Id = IPhone15ProId,
                Name = "iPhone 15 Pro",
                Description = "The latest iPhone with titanium case",
                Price = 1199.00m,
                Stock = 50,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "/images/products/iphone-15-pro.jpg"
            },
            new Product
            {
                Id = MacBookAirM3Id,
                Name = "MacBook Air M3",
                Description = "Lightweight notebook with Apple M3 chip",
                Price = 1299.00m,
                Stock = 30,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "/images/products/macbook-air-m3.jpg"
            },
            new Product
            {
                Id = GalaxyS24UltraId,
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Flagship smartphone with S Pen",
                Price = 1449.00m,
                Stock = 25,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "/images/products/galaxy-s24-ultra.jpg"
            },
            new Product
            {
                Id = PremiumHoodieId,
                Name = "Premium Hoodie",
                Description = "Comfortable hoodie made of organic cotton",
                Price = 79.99m,
                Stock = 100,
                CategoryId = InMemoryCategoryRepository.ClothingId,
                ImageUrl = "/images/products/premium-hoodie.jpg"
            },
            new Product
            {
                Id = DesignerJeansId,
                Name = "Designer Jeans",
                Description = "High-quality denim jeans",
                Price = 129.99m,
                Stock = 75,
                CategoryId = InMemoryCategoryRepository.ClothingId,
                ImageUrl = "/images/products/designer-jeans.jpg"
            },
            new Product
            {
                Id = CleanCodeId,
                Name = "Clean Code",
                Description = "Robert C. Martin - A Handbook of Agile Software Craftsmanship",
                Price = 39.99m,
                Stock = 200,
                CategoryId = InMemoryCategoryRepository.BooksId,
                ImageUrl = "/images/products/clean-code.jpg"
            },
            new Product
            {
                Id = DesignPatternsId,
                Name = "Design Patterns",
                Description = "Gang of Four - Elements of Reusable Object-Oriented Software",
                Price = 49.99m,
                Stock = 150,
                CategoryId = InMemoryCategoryRepository.BooksId,
                ImageUrl = "/images/products/design-patterns.jpg"
            },
            new Product
            {
                Id = CoffeeMachineDeluxeId,
                Name = "Coffee Machine Deluxe",
                Description = "Fully automatic espresso machine",
                Price = 599.00m,
                Stock = 20,
                CategoryId = InMemoryCategoryRepository.HouseholdId,
                ImageUrl = "/images/products/coffee-machine-deluxe.jpg"
            }
        };

        foreach (var product in products)
        {
            _products.TryAdd(product.Id, product);
        }
    }

    /// <inheritdoc />
    public Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    {
        _products.TryGetValue(id, out var product);
        return Task.FromResult(product);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values.ToList());
    }

    /// <inheritdoc />
    public Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = _products.Values
            .Where(p => p.CategoryId == categoryId)
            .ToList();
        return Task.FromResult<IEnumerable<Product>>(products);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Product>> Search(string searchTerm, CancellationToken cancellationToken)
    {
        var products = _products.Values
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       (p.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false))
            .ToList();
        return Task.FromResult<IEnumerable<Product>>(products);
    }

    /// <inheritdoc />
    public Task<Product> Add(Product entity, CancellationToken cancellationToken)
    {
        _products.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<Product> Update(Product entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _products[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_products.TryRemove(id, out _));
    }
}
