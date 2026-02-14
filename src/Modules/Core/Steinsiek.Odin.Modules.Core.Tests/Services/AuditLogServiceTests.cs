namespace Steinsiek.Odin.Modules.Core.Tests.Services;

/// <summary>
/// Unit tests for <see cref="AuditLogService"/>.
/// </summary>
[TestClass]
public sealed class AuditLogServiceTests
{
    private Mock<IAuditLogRepository> _repositoryMock = null!;
    private AuditLogService _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IAuditLogRepository>();
        _sut = new AuditLogService(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GetByEntity_ShouldReturnMappedDtos()
    {
        // Arrange
        var entry = Helpers.TestDataBuilders.CreateDefaultAuditLogEntry();
        _repositoryMock.Setup(r => r.GetByEntity("Person", entry.EntityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { entry });

        // Act
        var result = await _sut.GetByEntity("Person", entry.EntityId, CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual(entry.Id, result.Data[0].Id);
        Assert.AreEqual(entry.EntityType, result.Data[0].EntityType);
        Assert.AreEqual(entry.Action, result.Data[0].Action);
        Assert.AreEqual(entry.PropertyName, result.Data[0].PropertyName);
        Assert.AreEqual(entry.OldValue, result.Data[0].OldValue);
        Assert.AreEqual(entry.NewValue, result.Data[0].NewValue);
    }

    [TestMethod]
    public async Task GetByEntity_ShouldReturnEmptyList_WhenNoEntries()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByEntity(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<AuditLogEntry>());

        // Act
        var result = await _sut.GetByEntity("Person", Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.TotalCount);
        Assert.HasCount(0, result.Data);
    }

    [TestMethod]
    public async Task GetByEntity_ShouldSetCorrectTotalCount()
    {
        // Arrange
        var entries = new[]
        {
            Helpers.TestDataBuilders.CreateDefaultAuditLogEntry(),
            Helpers.TestDataBuilders.CreateAuditLogEntry_Created(),
            Helpers.TestDataBuilders.CreateAuditLogEntry_Deleted()
        };
        _repositoryMock.Setup(r => r.GetByEntity(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        var result = await _sut.GetByEntity("Person", Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.AreEqual(3, result.TotalCount);
        Assert.HasCount(3, result.Data);
    }

    [TestMethod]
    public async Task GetRecent_ShouldReturnMappedDtos()
    {
        // Arrange
        var entry = Helpers.TestDataBuilders.CreateDefaultAuditLogEntry();
        _repositoryMock.Setup(r => r.GetRecent(10, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { entry });

        // Act
        var result = await _sut.GetRecent(10, CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual(entry.Id, result.Data[0].Id);
        Assert.AreEqual(entry.Timestamp, result.Data[0].Timestamp);
    }

    [TestMethod]
    public async Task GetRecent_ShouldReturnEmptyList_WhenNoEntries()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<AuditLogEntry>());

        // Act
        var result = await _sut.GetRecent(10, CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.TotalCount);
        Assert.HasCount(0, result.Data);
    }

    [TestMethod]
    public async Task GetRecent_ShouldPassCountToRepository()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<AuditLogEntry>());

        // Act
        await _sut.GetRecent(42, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.GetRecent(42, It.IsAny<CancellationToken>()), Times.Once);
    }
}
