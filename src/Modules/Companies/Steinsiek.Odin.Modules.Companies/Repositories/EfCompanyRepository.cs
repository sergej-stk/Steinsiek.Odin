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

    /// <inheritdoc />
    public async Task<(IEnumerable<Company> Items, int TotalCount)> GetPaged(PagedQuery query, CompanyFilterQuery filter, CancellationToken cancellationToken)
    {
#pragma warning disable RCS1155 // ToLower is required for EF Core LINQ-to-SQL translation
        var queryable = _context.Set<Company>()
            .Include(c => c.Locations)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var normalizedTerm = query.Q.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            queryable = queryable.Where(c => c.Name.ToLower().Contains(normalizedTerm));
        }

        if (filter.IndustryId.HasValue)
        {
            queryable = queryable.Where(c => c.IndustryId == filter.IndustryId.Value);
        }

        if (filter.LegalFormId.HasValue)
        {
            queryable = queryable.Where(c => c.LegalFormId == filter.LegalFormId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            var normalizedCity = filter.City.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            queryable = queryable.Where(c => c.Locations.Any(l => l.City.ToLower().Contains(normalizedCity)));
        }

        if (filter.EmployeeCountMin.HasValue)
        {
            queryable = queryable.Where(c => c.EmployeeCount >= filter.EmployeeCountMin.Value);
        }

        if (filter.EmployeeCountMax.HasValue)
        {
            queryable = queryable.Where(c => c.EmployeeCount <= filter.EmployeeCountMax.Value);
        }

        if (filter.FoundingDateFrom.HasValue)
        {
            var from = DateTime.SpecifyKind(filter.FoundingDateFrom.Value, DateTimeKind.Utc);
            queryable = queryable.Where(c => c.FoundingDate >= from);
        }

        if (filter.FoundingDateTo.HasValue)
        {
            var to = DateTime.SpecifyKind(filter.FoundingDateTo.Value, DateTimeKind.Utc);
            queryable = queryable.Where(c => c.FoundingDate <= to);
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var desc = query.SortDir == SortDirection.Desc;

        if (string.Equals(query.Sort, nameof(CompanyDto.Name), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc ? queryable.OrderByDescending(c => c.Name) : queryable.OrderBy(c => c.Name);
        }
        else if (string.Equals(query.Sort, nameof(CompanyDto.City), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc
                ? queryable.OrderByDescending(c => c.Locations.OrderByDescending(l => l.IsPrimary).Select(l => l.City).FirstOrDefault())
                : queryable.OrderBy(c => c.Locations.OrderByDescending(l => l.IsPrimary).Select(l => l.City).FirstOrDefault());
        }
        else if (string.Equals(query.Sort, nameof(CompanyDto.EmployeeCount), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc ? queryable.OrderByDescending(c => c.EmployeeCount) : queryable.OrderBy(c => c.EmployeeCount);
        }
        else if (string.Equals(query.Sort, nameof(Company.FoundingDate), StringComparison.OrdinalIgnoreCase))
        {
            queryable = desc ? queryable.OrderByDescending(c => c.FoundingDate) : queryable.OrderBy(c => c.FoundingDate);
        }
        else
        {
            queryable = desc ? queryable.OrderByDescending(c => c.Name) : queryable.OrderBy(c => c.Name);
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
