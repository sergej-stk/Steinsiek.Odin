namespace Steinsiek.Odin.Modules.Core.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="AuditLogController"/>.
/// </summary>
[TestClass]
public sealed class AuditLogControllerTests
{
    private Mock<IAuditLogService> _serviceMock = null!;
    private AuditLogController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IAuditLogService>();
        _sut = new AuditLogController(_serviceMock.Object);
    }

    [TestMethod]
    public async Task GetRecent_ShouldReturnOk_WithAuditEntries()
    {
        // Arrange
        var expected = new ListResult<AuditLogDto>
        {
            TotalCount = 1,
            Data = [Helpers.TestDataBuilders.CreateDefaultAuditLogDto()]
        };
        _serviceMock.Setup(s => s.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _sut.GetRecent(5, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var value = okResult.Value as ListResult<AuditLogDto>;
        Assert.IsNotNull(value);
        Assert.AreEqual(1, value.TotalCount);
    }

    [TestMethod]
    public async Task GetRecent_ShouldDefaultToTen_WhenCountNotPositive()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<AuditLogDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetRecent(0, CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetRecent(10, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetRecent_ShouldPassCount_WhenPositive()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetRecent(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<AuditLogDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetRecent(25, CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetRecent(25, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetByEntity_ShouldReturnOk_WithAuditEntries()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var expected = new ListResult<AuditLogDto>
        {
            TotalCount = 1,
            Data = [Helpers.TestDataBuilders.CreateDefaultAuditLogDto()]
        };
        _serviceMock.Setup(s => s.GetByEntity("Person", entityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _sut.GetByEntity("Person", entityId, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
    }
}
