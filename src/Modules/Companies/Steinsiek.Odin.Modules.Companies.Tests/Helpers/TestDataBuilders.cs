namespace Steinsiek.Odin.Modules.Companies.Tests.Helpers;

/// <summary>
/// Provides factory methods for creating test data in Companies module tests.
/// </summary>
internal static class TestDataBuilders
{
    /// <summary>
    /// Well-known company identifier for test data.
    /// </summary>
    public static readonly Guid DefaultCompanyId = Guid.Parse("33333333-0001-0001-0001-000000000001");

    /// <summary>
    /// Creates a default company entity with a primary location.
    /// </summary>
    public static Company CreateDefaultCompany()
    {
        var companyId = DefaultCompanyId;
        return new Company
        {
            Id = companyId,
            Name = "Steinsiek GmbH",
            Website = "https://steinsiek.de",
            Email = "info@steinsiek.de",
            Phone = "+49 123 456789",
            EmployeeCount = 50,
            FoundingDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Locations =
            [
                new CompanyLocation
                {
                    Id = Guid.NewGuid(), CompanyId = companyId, Name = "HQ",
                    Street = "Main St 1", City = "Berlin", PostalCode = "10115",
                    CountryId = Guid.NewGuid(), IsPrimary = true
                }
            ],
            PersonCompanies = [],
            ChildCompanies = []
        };
    }

    /// <summary>
    /// Creates a minimal company entity without locations.
    /// </summary>
    public static Company CreateMinimalCompany()
    {
        return new Company
        {
            Id = Guid.NewGuid(),
            Name = "Minimal Corp",
            CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc),
            Locations = [],
            PersonCompanies = [],
            ChildCompanies = []
        };
    }

    /// <summary>
    /// Creates a default create company request.
    /// </summary>
    public static CreateCompanyRequest CreateDefaultCreateRequest()
    {
        return new CreateCompanyRequest
        {
            Name = "New Company",
            Website = "https://newcompany.com",
            Email = "info@newcompany.com"
        };
    }

    /// <summary>
    /// Creates a default update company request.
    /// </summary>
    public static UpdateCompanyRequest CreateDefaultUpdateRequest()
    {
        return new UpdateCompanyRequest
        {
            Name = "Updated Company",
            Website = "https://updated.com"
        };
    }

    /// <summary>
    /// Creates a default paged query.
    /// </summary>
    public static PagedQuery CreateDefaultPagedQuery()
    {
        return new PagedQuery
        {
            Page = 1,
            PageSize = 25
        };
    }

    /// <summary>
    /// Creates a default company filter query.
    /// </summary>
    public static CompanyFilterQuery CreateDefaultFilterQuery()
    {
        return new CompanyFilterQuery();
    }
}
