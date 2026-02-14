namespace Steinsiek.Odin.Modules.Core.Tests.Helpers;

/// <summary>
/// Extension methods for fluent mock setup in Core module tests.
/// </summary>
internal static class MockSetupExtensions
{
    /// <summary>
    /// Sets up <see cref="IAuditLogRepository.GetByEntity"/> to return the given entries.
    /// </summary>
    public static Mock<IAuditLogRepository> SetupGetByEntity_Returns(
        this Mock<IAuditLogRepository> mock,
        IEnumerable<AuditLogEntry> entries)
    {
        mock.Setup(r => r.GetByEntity(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IAuditLogRepository.GetRecent"/> to return the given entries.
    /// </summary>
    public static Mock<IAuditLogRepository> SetupGetRecent_Returns(
        this Mock<IAuditLogRepository> mock,
        IEnumerable<AuditLogEntry> entries)
    {
        mock.Setup(r => r.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);
        return mock;
    }
}
