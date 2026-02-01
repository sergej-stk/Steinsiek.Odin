namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// In-memory implementation of the product repository for development and testing.
/// </summary>
public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();

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
                Id = Guid.NewGuid(),
                Name = "iPhone 15 Pro",
                Description = "The latest iPhone with titanium case",
                Price = 1199.00m,
                Stock = 50,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "https://example.com/iphone15pro.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Air M3",
                Description = "Lightweight notebook with Apple M3 chip",
                Price = 1299.00m,
                Stock = 30,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "https://example.com/macbook-air-m3.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Flagship smartphone with S Pen",
                Price = 1449.00m,
                Stock = 25,
                CategoryId = InMemoryCategoryRepository.ElectronicsId,
                ImageUrl = "https://example.com/galaxy-s24.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Premium Hoodie",
                Description = "Comfortable hoodie made of organic cotton",
                Price = 79.99m,
                Stock = 100,
                CategoryId = InMemoryCategoryRepository.ClothingId,
                ImageUrl = "https://example.com/hoodie.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Designer Jeans",
                Description = "High-quality denim jeans",
                Price = 129.99m,
                Stock = 75,
                CategoryId = InMemoryCategoryRepository.ClothingId,
                ImageUrl = "https://example.com/jeans.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Clean Code",
                Description = "Robert C. Martin - A Handbook of Agile Software Craftsmanship",
                Price = 39.99m,
                Stock = 200,
                CategoryId = InMemoryCategoryRepository.BooksId,
                ImageUrl = "https://example.com/clean-code.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Design Patterns",
                Description = "Gang of Four - Elements of Reusable Object-Oriented Software",
                Price = 49.99m,
                Stock = 150,
                CategoryId = InMemoryCategoryRepository.BooksId,
                ImageUrl = "https://example.com/design-patterns.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coffee Machine Deluxe",
                Description = "Fully automatic espresso machine",
                Price = 599.00m,
                Stock = 20,
                CategoryId = InMemoryCategoryRepository.HouseholdId,
                ImageUrl = "https://example.com/coffee-machine.jpg"
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
