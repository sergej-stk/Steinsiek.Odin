namespace Steinsiek.Odin.Modules.Products.Repositories;

/// <summary>
/// Entity Framework Core implementation of the product image repository.
/// </summary>
public sealed class EfProductImageRepository(OdinDbContext context) : IProductImageRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<ProductImageData?> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await _context.Set<ProductImageData>().FindAsync([productId], cancellationToken);
    }

    /// <inheritdoc />
    public async Task Upsert(ProductImageData imageData, CancellationToken cancellationToken)
    {
        var existing = await _context.Set<ProductImageData>().FindAsync([imageData.ProductId], cancellationToken);
        if (existing is not null)
        {
            existing.ContentType = imageData.ContentType;
            existing.FileName = imageData.FileName;
            existing.Data = imageData.Data;
        }
        else
        {
            _context.Set<ProductImageData>().Add(imageData);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid productId, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductImageData>().FindAsync([productId], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<ProductImageData>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Exists(Guid productId, CancellationToken cancellationToken)
    {
        return await _context.Set<ProductImageData>().AnyAsync(i => i.ProductId == productId, cancellationToken);
    }
}
