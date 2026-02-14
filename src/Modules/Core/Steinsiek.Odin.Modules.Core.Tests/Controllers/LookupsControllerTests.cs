namespace Steinsiek.Odin.Modules.Core.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="LookupsController"/>.
/// </summary>
[TestClass]
public sealed class LookupsControllerTests
{
    private Mock<ITranslationService> _translationServiceMock = null!;
    private Mock<ILogger<LookupsController>> _loggerMock = null!;
    private LookupsController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _translationServiceMock = new Mock<ITranslationService>();
        _loggerMock = new Mock<ILogger<LookupsController>>();
        _sut = new LookupsController(_translationServiceMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetByType_ShouldReturnOk_WithLookups()
    {
        // Arrange
        var lookups = new ListResult<LookupDto>
        {
            TotalCount = 1,
            Data = [Helpers.TestDataBuilders.CreateDefaultLookupDto()]
        };
        _translationServiceMock.Setup(s => s.GetLookups("salutation", It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookups);

        // Act
        var result = await _sut.GetByType("salutation", "en", CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [TestMethod]
    public async Task GetByType_ShouldUseDefaultLanguage_WhenLangIsNull()
    {
        // Arrange
        _translationServiceMock.Setup(s => s.GetLookups(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<LookupDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetByType("salutation", null, CancellationToken.None);

        // Assert
        _translationServiceMock.Verify(
            s => s.GetLookups("salutation", LanguageCodes.Default, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [TestMethod]
    public async Task GetByType_ShouldPassProvidedLanguage_WhenSpecified()
    {
        // Arrange
        _translationServiceMock.Setup(s => s.GetLookups(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListResult<LookupDto> { TotalCount = 0, Data = [] });

        // Act
        await _sut.GetByType("gender", "en", CancellationToken.None);

        // Assert
        _translationServiceMock.Verify(
            s => s.GetLookups("gender", "en", It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
