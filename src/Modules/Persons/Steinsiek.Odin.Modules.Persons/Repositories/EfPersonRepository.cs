namespace Steinsiek.Odin.Modules.Persons.Repositories;

/// <summary>
/// Entity Framework Core implementation of the person repository.
/// </summary>
public sealed class EfPersonRepository(OdinDbContext context) : IPersonRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<Person?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Person>()
            .Include(p => p.Addresses)
            .Include(p => p.EmailAddresses)
            .Include(p => p.PhoneNumbers)
            .Include(p => p.BankAccounts)
            .Include(p => p.SocialMediaLinks)
            .Include(p => p.Languages)
            .Include(p => p.Image)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Person>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<Person>()
            .Include(p => p.Addresses)
            .Include(p => p.EmailAddresses)
            .Include(p => p.PhoneNumbers)
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Person> Add(Person entity, CancellationToken cancellationToken)
    {
        _context.Set<Person>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<Person> Update(Person entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<Person>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<Person>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<Person>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Person>> Search(string searchTerm, CancellationToken cancellationToken)
    {
#pragma warning disable RCS1155 // ToLower is required for EF Core LINQ-to-SQL translation
        var normalizedTerm = searchTerm.ToLower(System.Globalization.CultureInfo.InvariantCulture);
        return await _context.Set<Person>()
            .Include(p => p.Addresses)
            .Include(p => p.EmailAddresses)
            .Include(p => p.PhoneNumbers)
            .Where(p => p.FirstName.ToLower().Contains(normalizedTerm)
                        || p.LastName.ToLower().Contains(normalizedTerm))
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync(cancellationToken);
#pragma warning restore RCS1155
    }
}
