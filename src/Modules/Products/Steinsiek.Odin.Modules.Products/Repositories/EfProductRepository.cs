namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Entity Framework Core implementation of the product repository.
/// </summary>
public sealed class EfProductRepository(OdinDbContext context) : IProductRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Product>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<Product>().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetByCategoryId(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Set<Product>()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Search(string searchTerm, CancellationToken cancellationToken)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        return await _context.Set<Product>()
            .Where(p => p.Name.ToLower().Contains(lowerSearchTerm) ||
                       (p.Description != null && p.Description.ToLower().Contains(lowerSearchTerm)))
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Product> Add(Product entity, CancellationToken cancellationToken)
    {
        _context.Set<Product>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<Product> Update(Product entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Product>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Product>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<Product>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
