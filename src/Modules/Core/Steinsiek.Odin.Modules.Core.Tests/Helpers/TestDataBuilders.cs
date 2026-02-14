namespace Steinsiek.Odin.Modules.Core.Tests.Helpers;

/// <summary>
/// Provides factory methods for creating test data in Core module tests.
/// </summary>
internal static class TestDataBuilders
{
    /// <summary>
    /// Well-known entity identifier for test data.
    /// </summary>
    public static readonly Guid DefaultEntityId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

    /// <summary>
    /// Well-known user identifier for test data.
    /// </summary>
    public static readonly Guid DefaultUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    /// <summary>
    /// Creates a default audit log entry for "Updated" actions.
    /// </summary>
    public static AuditLogEntry CreateDefaultAuditLogEntry()
    {
        return new AuditLogEntry
        {
            Id = Guid.NewGuid(),
            EntityType = "Person",
            EntityId = DefaultEntityId,
            Action = "Updated",
            PropertyName = "FirstName",
            OldValue = "John",
            NewValue = "Jane",
            UserId = DefaultUserId,
            Timestamp = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc)
        };
    }

    /// <summary>
    /// Creates an audit log entry for "Created" actions.
    /// </summary>
    public static AuditLogEntry CreateAuditLogEntry_Created()
    {
        return new AuditLogEntry
        {
            Id = Guid.NewGuid(),
            EntityType = "Company",
            EntityId = Guid.NewGuid(),
            Action = "Created",
            UserId = DefaultUserId,
            Timestamp = new DateTime(2026, 1, 15, 11, 0, 0, DateTimeKind.Utc)
        };
    }

    /// <summary>
    /// Creates an audit log entry for "Deleted" actions.
    /// </summary>
    public static AuditLogEntry CreateAuditLogEntry_Deleted()
    {
        return new AuditLogEntry
        {
            Id = Guid.NewGuid(),
            EntityType = "Person",
            EntityId = Guid.NewGuid(),
            Action = "Deleted",
            UserId = DefaultUserId,
            Timestamp = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc)
        };
    }

    /// <summary>
    /// Creates a default <see cref="AuditLogDto"/>.
    /// </summary>
    public static AuditLogDto CreateDefaultAuditLogDto()
    {
        return new AuditLogDto
        {
            Id = Guid.NewGuid(),
            EntityType = "Person",
            EntityId = DefaultEntityId,
            Action = "Updated",
            PropertyName = "FirstName",
            OldValue = "John",
            NewValue = "Jane",
            UserId = DefaultUserId,
            Timestamp = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc)
        };
    }

    /// <summary>
    /// Creates a default <see cref="LookupDto"/>.
    /// </summary>
    public static LookupDto CreateDefaultLookupDto()
    {
        return new LookupDto
        {
            Id = Guid.NewGuid(),
            Code = "MR",
            Value = "Mr.",
            SortOrder = 1
        };
    }
}
