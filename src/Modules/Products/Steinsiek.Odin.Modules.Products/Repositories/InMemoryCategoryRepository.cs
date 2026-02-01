namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// In-memory implementation of the category repository for development and testing.
/// </summary>
public sealed class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly ConcurrentDictionary<Guid, Category> _categories = new();

    /// <summary>
    /// Predefined identifier for the Electronics category.
    /// </summary>
    public static readonly Guid ElectronicsId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    /// <summary>
    /// Predefined identifier for the Clothing category.
    /// </summary>
    public static readonly Guid ClothingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

    /// <summary>
    /// Predefined identifier for the Books category.
    /// </summary>
    public static readonly Guid BooksId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

    /// <summary>
    /// Predefined identifier for the Household category.
    /// </summary>
    public static readonly Guid HouseholdId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

    /// <summary>
    /// Initializes a new instance and seeds demo data.
    /// </summary>
    public InMemoryCategoryRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var categories = new[]
        {
            new Category { Id = ElectronicsId, Name = "Electronics", Description = "Smartphones, laptops and more" },
            new Category { Id = ClothingId, Name = "Clothing", Description = "Fashion for every occasion" },
            new Category { Id = BooksId, Name = "Books", Description = "Non-fiction and fiction" },
            new Category { Id = HouseholdId, Name = "Household", Description = "Everything for your home" }
        };

        foreach (var category in categories)
        {
            _categories.TryAdd(category.Id, category);
        }
    }

    /// <inheritdoc />
    public Task<Category?> GetById(Guid id, CancellationToken cancellationToken)
    {
        _categories.TryGetValue(id, out var category);
        return Task.FromResult(category);
    }

    /// <inheritdoc />
    public Task<Category?> GetByName(string name, CancellationToken cancellationToken)
    {
        var category = _categories.Values.FirstOrDefault(c =>
            c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(category);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Category>>(_categories.Values.ToList());
    }

    /// <inheritdoc />
    public Task<Category> Add(Category entity, CancellationToken cancellationToken)
    {
        _categories.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<Category> Update(Category entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _categories[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_categories.TryRemove(id, out _));
    }
}
