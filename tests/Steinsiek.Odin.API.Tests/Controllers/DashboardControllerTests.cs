namespace Steinsiek.Odin.API.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="DashboardController"/>.
/// </summary>
[TestClass]
public sealed class DashboardControllerTests
{
    private Mock<IDashboardService> _serviceMock = null!;
    private Mock<ILogger<DashboardController>> _loggerMock = null!;
    private DashboardController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IDashboardService>();
        _loggerMock = new Mock<ILogger<DashboardController>>();
        _sut = new DashboardController(_serviceMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetStats_ShouldReturnOk_WithStats()
    {
        // Arrange
        var stats = Helpers.TestDataBuilders.CreateDefaultDashboardStats();
        _serviceMock.Setup(s => s.GetStats(It.IsAny<CancellationToken>())).ReturnsAsync(stats);

        // Act
        var result = await _sut.GetStats(CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(100, result.Value.TotalPersons);
        Assert.AreEqual(25, result.Value.TotalCompanies);
    }

    [TestMethod]
    public async Task GetRecentActivity_ShouldReturnOk_WithEntries()
    {
        // Arrange
        var activity = Helpers.TestDataBuilders.CreateDefaultRecentActivity();
        _serviceMock.Setup(s => s.GetRecentActivity(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(activity);

        // Act
        var result = await _sut.GetRecentActivity(5, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(2, result.Value.TotalCount);
    }

    [TestMethod]
    public async Task GetRecentActivity_ShouldDefaultToTen_WhenNotSpecified()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetRecentActivity(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<AuditLogDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetRecentActivity(cancellationToken: CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetRecentActivity(10, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetUpcomingBirthdays_ShouldReturnOk_WithPersons()
    {
        // Arrange
        var birthdays = Helpers.TestDataBuilders.CreateDefaultBirthdayList();
        _serviceMock.Setup(s => s.GetUpcomingBirthdays(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(birthdays);

        // Act
        var result = await _sut.GetUpcomingBirthdays(14, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(1, result.Value.TotalCount);
    }

    [TestMethod]
    public async Task GetUpcomingBirthdays_ShouldDefaultToSeven_WhenNotSpecified()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetUpcomingBirthdays(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<PersonDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetUpcomingBirthdays(cancellationToken: CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetUpcomingBirthdays(7, It.IsAny<CancellationToken>()), Times.Once);
    }
}
