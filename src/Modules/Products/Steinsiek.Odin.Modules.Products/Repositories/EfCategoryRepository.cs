namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Entity Framework Core implementation of the category repository.
/// </summary>
public sealed class EfCategoryRepository(OdinDbContext context) : ICategoryRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<Category?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Category>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Category?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Set<Category>()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<Category>().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Category> Add(Category entity, CancellationToken cancellationToken)
    {
        _context.Set<Category>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<Category> Update(Category entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Category>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Category>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<Category>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
