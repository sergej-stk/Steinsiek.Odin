namespace Steinsiek.Odin.API.Services;

/// <summary>
/// Service implementation for aggregating dashboard data across modules.
/// Resides in the API project because it crosses module boundaries.
/// </summary>
public sealed class DashboardService(
    OdinDbContext dbContext,
    Steinsiek.Odin.Modules.Core.Services.IAuditLogService auditLogService,
    ILogger<DashboardService> logger) : IDashboardService
{
    private readonly OdinDbContext _dbContext = dbContext;
    private readonly Steinsiek.Odin.Modules.Core.Services.IAuditLogService _auditLogService = auditLogService;
    private readonly ILogger<DashboardService> _logger = logger;

    /// <inheritdoc />
    public async Task<DashboardStatsDto> GetStats(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retrieving dashboard statistics");

        var now = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var totalPersons = await _dbContext
            .Set<Steinsiek.Odin.Modules.Persons.Entities.Person>()
            .CountAsync(cancellationToken);

        var totalCompanies = await _dbContext
            .Set<Steinsiek.Odin.Modules.Companies.Entities.Company>()
            .CountAsync(cancellationToken);

        var activeAssignments = await _dbContext
            .Set<Steinsiek.Odin.Modules.Companies.Entities.PersonCompany>()
            .CountAsync(pc => pc.IsActive, cancellationToken);

        var newPersonsThisMonth = await _dbContext
            .Set<Steinsiek.Odin.Modules.Persons.Entities.Person>()
            .CountAsync(p => p.CreatedAt >= monthStart, cancellationToken);

        var newCompaniesThisMonth = await _dbContext
            .Set<Steinsiek.Odin.Modules.Companies.Entities.Company>()
            .CountAsync(c => c.CreatedAt >= monthStart, cancellationToken);

        return new DashboardStatsDto
        {
            TotalPersons = totalPersons,
            TotalCompanies = totalCompanies,
            ActiveAssignments = activeAssignments,
            NewPersonsThisMonth = newPersonsThisMonth,
            NewCompaniesThisMonth = newCompaniesThisMonth
        };
    }

    /// <inheritdoc />
    public async Task<ListResult<AuditLogDto>> GetRecentActivity(int count, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retrieving {Count} recent audit log entries for dashboard", count);

        return await _auditLogService.GetRecent(count, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ListResult<PersonDto>> GetUpcomingBirthdays(int days, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retrieving upcoming birthdays within {Days} days", days);

        var today = DateTime.UtcNow.Date;

        var persons = await _dbContext
            .Set<Steinsiek.Odin.Modules.Persons.Entities.Person>()
            .Where(p => p.DateOfBirth != null)
            .ToListAsync(cancellationToken);

        var upcomingBirthdays = persons
            .Where(p =>
            {
                var dob = p.DateOfBirth!.Value;
                var birthdayThisYear = new DateTime(today.Year, dob.Month, dob.Day);
                if (birthdayThisYear < today)
                {
                    birthdayThisYear = birthdayThisYear.AddYears(1);
                }
                return (birthdayThisYear - today).TotalDays <= days;
            })
            .OrderBy(p =>
            {
                var dob = p.DateOfBirth!.Value;
                var birthdayThisYear = new DateTime(today.Year, dob.Month, dob.Day);
                if (birthdayThisYear < today)
                {
                    birthdayThisYear = birthdayThisYear.AddYears(1);
                }
                return birthdayThisYear;
            })
            .Select(p =>
            {
                var primaryEmail = p.EmailAddresses.FirstOrDefault(e => e.IsPrimary)
                    ?? p.EmailAddresses.FirstOrDefault();
                var primaryPhone = p.PhoneNumbers.FirstOrDefault(ph => ph.IsPrimary)
                    ?? p.PhoneNumbers.FirstOrDefault();
                var primaryAddress = p.Addresses.FirstOrDefault(a => a.IsPrimary)
                    ?? p.Addresses.FirstOrDefault();

                return new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Title = p.Title,
                    Email = primaryEmail?.Email,
                    Phone = primaryPhone?.Number,
                    City = primaryAddress?.City
                };
            })
            .ToList();

        return new ListResult<PersonDto>
        {
            TotalCount = upcomingBirthdays.Count,
            Data = upcomingBirthdays
        };
    }
}
