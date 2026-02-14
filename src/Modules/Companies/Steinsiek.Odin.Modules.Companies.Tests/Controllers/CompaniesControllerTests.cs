namespace Steinsiek.Odin.Modules.Companies.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="CompaniesController"/>.
/// </summary>
[TestClass]
public sealed class CompaniesControllerTests
{
    private Mock<ICompanyService> _serviceMock = null!;
    private Mock<ILogger<CompaniesController>> _loggerMock = null!;
    private CompaniesController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<ICompanyService>();
        _loggerMock = new Mock<ILogger<CompaniesController>>();
        _sut = new CompaniesController(_serviceMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnOk_WithCompanies()
    {
        // Arrange
        var expected = new ListResult<CompanyDto>
        {
            TotalCount = 1,
            Data = [new CompanyDto { Id = Guid.NewGuid(), Name = "Steinsiek GmbH" }]
        };
        _serviceMock.Setup(s => s.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _sut.GetAll(CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [TestMethod]
    public async Task GetPaged_ShouldReturnOk_WithPagedResult()
    {
        // Arrange
        var expected = new PagedResult<CompanyDto>
        {
            TotalCount = 1,
            Data = [new CompanyDto { Id = Guid.NewGuid(), Name = "Steinsiek GmbH" }],
            CurrentPage = 1,
            PageSize = 25
        };
        _serviceMock.Setup(s => s.GetPaged(It.IsAny<PagedQuery>(), It.IsAny<CompanyFilterQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _sut.GetPaged(new PagedQuery(), new CompanyFilterQuery(), CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        // Arrange
        var detail = new CompanyDetailDto
        {
            Id = Guid.NewGuid(),
            Name = "Steinsiek GmbH",
            Locations = [],
            PersonCompanies = []
        };
        _serviceMock.Setup(s => s.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(detail);

        // Act
        var result = await _sut.GetById(detail.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(detail.Id, result.Value.Id);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnNotFound_WhenNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CompanyDetailDto?)null);

        // Act
        var result = await _sut.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }

    [TestMethod]
    public async Task Search_ShouldReturnOk_WithMatchingCompanies()
    {
        // Arrange
        var expected = new ListResult<CompanyDto>
        {
            TotalCount = 1,
            Data = [new CompanyDto { Id = Guid.NewGuid(), Name = "Steinsiek GmbH" }]
        };
        _serviceMock.Setup(s => s.Search("Steinsiek", It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _sut.Search("Steinsiek", CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
    }

    [TestMethod]
    public async Task Search_ShouldReturnEmptyList_WhenQueryIsEmpty()
    {
        // Act
        var result = await _sut.Search("", CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var value = okResult.Value as ListResult<CompanyDto>;
        Assert.IsNotNull(value);
        Assert.AreEqual(0, value.TotalCount);
        _serviceMock.Verify(s => s.Search(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Search_ShouldReturnEmptyList_WhenQueryIsNull()
    {
        // Act
        var result = await _sut.Search(null, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var value = okResult.Value as ListResult<CompanyDto>;
        Assert.IsNotNull(value);
        Assert.AreEqual(0, value.TotalCount);
    }

    [TestMethod]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CompanyDto { Id = Guid.NewGuid(), Name = "New Company" };
        _serviceMock.Setup(s => s.Create(It.IsAny<CreateCompanyRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        // Act
        var result = await _sut.Create(
            Helpers.TestDataBuilders.CreateDefaultCreateRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<CreatedAtActionResult>(result.Result);
    }

    [TestMethod]
    public async Task Update_ShouldReturnOk_WhenFound()
    {
        // Arrange
        var dto = new CompanyDto { Id = Guid.NewGuid(), Name = "Updated Company" };
        _serviceMock.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<UpdateCompanyRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        // Act
        var result = await _sut.Update(dto.Id, Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual("Updated Company", result.Value.Name);
    }

    [TestMethod]
    public async Task Update_ShouldReturnNotFound_WhenNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<UpdateCompanyRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CompanyDto?)null);

        // Act
        var result = await _sut.Update(Guid.NewGuid(), Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }

    [TestMethod]
    public async Task Delete_ShouldReturnNoContent_WhenFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _sut.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
    }

    [TestMethod]
    public async Task Delete_ShouldReturnNotFound_WhenNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _sut.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(result);
    }

    [TestMethod]
    public async Task DeleteMany_ShouldReturnDeletedCount()
    {
        // Arrange
        _serviceMock.Setup(s => s.DeleteMany(It.IsAny<IReadOnlyList<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        // Act
        var result = await _sut.DeleteMany(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.Value);
    }
}
