namespace Steinsiek.Odin.API.Tests.Helpers;

/// <summary>
/// Provides factory methods for creating test data in API tests.
/// </summary>
internal static class TestDataBuilders
{
    /// <summary>
    /// Creates a default dashboard stats DTO.
    /// </summary>
    public static DashboardStatsDto CreateDefaultDashboardStats()
    {
        return new DashboardStatsDto
        {
            TotalPersons = 100,
            TotalCompanies = 25,
            ActiveAssignments = 50,
            NewPersonsThisMonth = 10,
            NewCompaniesThisMonth = 3
        };
    }

    /// <summary>
    /// Creates a default list result of audit log DTOs for recent activity.
    /// </summary>
    public static ListResult<AuditLogDto> CreateDefaultRecentActivity()
    {
        return new ListResult<AuditLogDto>
        {
            TotalCount = 2,
            Data =
            [
                new AuditLogDto
                {
                    Id = Guid.NewGuid(),
                    EntityType = "Person",
                    EntityId = Guid.NewGuid(),
                    Action = "Created",
                    Timestamp = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc)
                },
                new AuditLogDto
                {
                    Id = Guid.NewGuid(),
                    EntityType = "Company",
                    EntityId = Guid.NewGuid(),
                    Action = "Updated",
                    PropertyName = "Name",
                    OldValue = "Old Corp",
                    NewValue = "New Corp",
                    Timestamp = new DateTime(2026, 1, 15, 11, 0, 0, DateTimeKind.Utc)
                }
            ]
        };
    }

    /// <summary>
    /// Creates a default list result of person DTOs for upcoming birthdays.
    /// </summary>
    public static ListResult<PersonDto> CreateDefaultBirthdayList()
    {
        return new ListResult<PersonDto>
        {
            TotalCount = 1,
            Data =
            [
                new PersonDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Max",
                    LastName = "Mustermann"
                }
            ]
        };
    }
}
