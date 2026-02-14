namespace Steinsiek.Odin.Modules.Companies.Repositories;

/// <summary>
/// Entity Framework Core implementation of the company repository.
/// </summary>
public sealed class EfCompanyRepository(OdinDbContext context) : ICompanyRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<Company?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Company>()
            .Include(c => c.Locations)
            .Include(c => c.PersonCompanies)
            .Include(c => c.Image)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Company>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<Company>()
            .Include(c => c.Locations)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Company> Add(Company entity, CancellationToken cancellationToken)
    {
        _context.Set<Company>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<Company> Update(Company entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Company>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Company>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<Company>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Company>> Search(string searchTerm, CancellationToken cancellationToken)
    {
#pragma warning disable RCS1155
        var normalizedTerm = searchTerm.ToLower();
        return await _context.Set<Company>()
            .Include(c => c.Locations)
            .Where(c => c.Name.ToLower().Contains(normalizedTerm))
            .ToListAsync(cancellationToken);
#pragma warning restore RCS1155
    }
}
