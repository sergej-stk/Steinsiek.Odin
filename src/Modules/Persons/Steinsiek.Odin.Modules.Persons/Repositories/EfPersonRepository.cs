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

    /// <inheritdoc />
    public async Task<(IEnumerable<Person> Items, int TotalCount)> GetPaged(PagedQuery query, PersonFilterQuery filter, CancellationToken cancellationToken)
    {
#pragma warning disable RCS1155 // ToLower is required for EF Core LINQ-to-SQL translation
        var queryable = _context.Set<Person>()
            .Include(p => p.Addresses)
            .Include(p => p.EmailAddresses)
            .Include(p => p.PhoneNumbers)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var normalizedTerm = query.Q.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            queryable = queryable.Where(p => p.FirstName.ToLower().Contains(normalizedTerm)
                                             || p.LastName.ToLower().Contains(normalizedTerm));
        }

        if (filter.SalutationId.HasValue)
        {
            queryable = queryable.Where(p => p.SalutationId == filter.SalutationId.Value);
        }

        if (filter.GenderId.HasValue)
        {
            queryable = queryable.Where(p => p.GenderId == filter.GenderId.Value);
        }

        if (filter.NationalityId.HasValue)
        {
            queryable = queryable.Where(p => p.NationalityId == filter.NationalityId.Value);
        }

        if (filter.MaritalStatusId.HasValue)
        {
            queryable = queryable.Where(p => p.MaritalStatusId == filter.MaritalStatusId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            var normalizedCity = filter.City.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            queryable = queryable.Where(p => p.Addresses.Any(a => a.City.ToLower().Contains(normalizedCity)));
        }

        if (filter.CreatedFrom.HasValue)
        {
            var from = DateTime.SpecifyKind(filter.CreatedFrom.Value, DateTimeKind.Utc);
            queryable = queryable.Where(p => p.CreatedAt >= from);
        }

        if (filter.CreatedTo.HasValue)
        {
            var to = DateTime.SpecifyKind(filter.CreatedTo.Value, DateTimeKind.Utc);
            queryable = queryable.Where(p => p.CreatedAt <= to);
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var desc = query.SortDir == SortDirection.Desc;

        if (string.Equals(query.Sort, nameof(PersonDto.FirstName), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc ? queryable.OrderByDescending(p => p.FirstName) : queryable.OrderBy(p => p.FirstName);
        }
        else if (string.Equals(query.Sort, nameof(PersonDto.Email), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc
                ? queryable.OrderByDescending(p => p.EmailAddresses.OrderByDescending(e => e.IsPrimary).Select(e => e.Email).FirstOrDefault())
                : queryable.OrderBy(p => p.EmailAddresses.OrderByDescending(e => e.IsPrimary).Select(e => e.Email).FirstOrDefault());
        }
        else if (string.Equals(query.Sort, nameof(PersonDto.City), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc
                ? queryable.OrderByDescending(p => p.Addresses.OrderByDescending(a => a.IsPrimary).Select(a => a.City).FirstOrDefault())
                : queryable.OrderBy(p => p.Addresses.OrderByDescending(a => a.IsPrimary).Select(a => a.City).FirstOrDefault());
        }
        else if (string.Equals(query.Sort, nameof(Person.CreatedAt), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc ? queryable.OrderByDescending(p => p.CreatedAt) : queryable.OrderBy(p => p.CreatedAt);
        }
        else
        {
            queryable = desc
                ? queryable.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName)
                : queryable.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
        }

        var page = Math.Max(1, query.Page);
        var items = await queryable
            .Skip((page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
#pragma warning restore RCS1155
    }
}
