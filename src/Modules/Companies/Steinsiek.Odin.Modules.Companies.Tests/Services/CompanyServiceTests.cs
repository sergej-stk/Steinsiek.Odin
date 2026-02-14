namespace Steinsiek.Odin.Modules.Companies.Tests.Services;

/// <summary>
/// Unit tests for <see cref="CompanyService"/>.
/// </summary>
[TestClass]
public sealed class CompanyServiceTests
{
    private Mock<ICompanyRepository> _repoMock = null!;
    private Mock<ILogger<CompanyService>> _loggerMock = null!;
    private CompanyService _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _repoMock = new Mock<ICompanyRepository>();
        _loggerMock = new Mock<ILogger<CompanyService>>();
        _sut = new CompanyService(_repoMock.Object, _loggerMock.Object);
    }

    // --- GetAll ---

    [TestMethod]
    public async Task GetAll_ShouldReturnAllCompanies()
    {
        // Arrange
        var companies = new[] { Helpers.TestDataBuilders.CreateDefaultCompany(), Helpers.TestDataBuilders.CreateMinimalCompany() };
        _repoMock.SetupGetAll_Returns(companies);

        // Act
        var result = await _sut.GetAll(CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.TotalCount);
        Assert.HasCount(2, result.Data);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoCompanies()
    {
        // Arrange
        _repoMock.SetupGetAll_Returns(Enumerable.Empty<Company>());

        // Act
        var result = await _sut.GetAll(CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.TotalCount);
    }

    // --- GetById ---

    [TestMethod]
    public async Task GetById_ShouldReturnDetailDto_WhenCompanyExists()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupGetById_Returns(company);

        // Act
        var result = await _sut.GetById(company.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(company.Id, result.Id);
        Assert.AreEqual(company.Name, result.Name);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnNull_WhenCompanyNotFound()
    {
        // Arrange
        _repoMock.SetupGetById_Returns((Company?)null);

        // Act
        var result = await _sut.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetById_ShouldMapLocations()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupGetById_Returns(company);

        // Act
        var result = await _sut.GetById(company.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.HasCount(1, result.Locations);
        Assert.AreEqual("HQ", result.Locations[0].Name);
    }

    // --- Create ---

    [TestMethod]
    public async Task Create_ShouldReturnCreatedCompanyDto()
    {
        // Arrange
        _repoMock.SetupAdd_ReturnsInput();
        var request = Helpers.TestDataBuilders.CreateDefaultCreateRequest();

        // Act
        var result = await _sut.Create(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("New Company", result.Name);
    }

    [TestMethod]
    public async Task Create_ShouldCallRepositoryAdd()
    {
        // Arrange
        _repoMock.SetupAdd_ReturnsInput();

        // Act
        await _sut.Create(Helpers.TestDataBuilders.CreateDefaultCreateRequest(), CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Add(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Create_ShouldMapRequestFieldsToEntity()
    {
        // Arrange
        _repoMock.SetupAdd_ReturnsInput();
        var request = Helpers.TestDataBuilders.CreateDefaultCreateRequest();

        // Act
        await _sut.Create(request, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Add(
            It.Is<Company>(c => c.Name == "New Company" && c.Website == "https://newcompany.com"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // --- Update ---

    [TestMethod]
    public async Task Update_ShouldReturnUpdatedDto_WhenCompanyExists()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupGetById_Returns(company);
        _repoMock.SetupUpdate_ReturnsInput();

        // Act
        var result = await _sut.Update(company.Id, Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Updated Company", result.Name);
    }

    [TestMethod]
    public async Task Update_ShouldReturnNull_WhenCompanyNotFound()
    {
        // Arrange
        _repoMock.SetupGetById_Returns((Company?)null);

        // Act
        var result = await _sut.Update(Guid.NewGuid(), Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Update_ShouldApplyRequestFieldsToEntity()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupGetById_Returns(company);
        _repoMock.SetupUpdate_ReturnsInput();

        // Act
        await _sut.Update(company.Id, Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Update(
            It.Is<Company>(c => c.Name == "Updated Company" && c.Website == "https://updated.com"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // --- Delete ---

    [TestMethod]
    public async Task Delete_ShouldReturnTrue_WhenCompanyExists()
    {
        // Arrange
        _repoMock.SetupDelete_Returns(true);

        // Act
        var result = await _sut.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Delete_ShouldReturnFalse_WhenCompanyNotFound()
    {
        // Arrange
        _repoMock.SetupDelete_Returns(false);

        // Act
        var result = await _sut.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    // --- Search ---

    [TestMethod]
    public async Task Search_ShouldReturnMatchingCompanies()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupSearch_Returns(new[] { company });

        // Act
        var result = await _sut.Search("Steinsiek", CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual("Steinsiek GmbH", result.Data[0].Name);
    }

    [TestMethod]
    public async Task Search_ShouldReturnEmptyList_WhenNoMatches()
    {
        // Arrange
        _repoMock.SetupSearch_Returns(Enumerable.Empty<Company>());

        // Act
        var result = await _sut.Search("NonExistent", CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.TotalCount);
    }

    // --- GetPaged ---

    [TestMethod]
    public async Task GetPaged_ShouldReturnPagedResult()
    {
        // Arrange
        var company = Helpers.TestDataBuilders.CreateDefaultCompany();
        _repoMock.SetupGetPaged_Returns(new[] { company }, 1);

        // Act
        var result = await _sut.GetPaged(Helpers.TestDataBuilders.CreateDefaultPagedQuery(), Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual(1, result.CurrentPage);
        Assert.AreEqual(25, result.PageSize);
    }

    [TestMethod]
    public async Task GetPaged_ShouldClampInvalidPageSize()
    {
        // Arrange
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Company>(), 0);
        var query = new PagedQuery { Page = 1, PageSize = 999 };

        // Act
        var result = await _sut.GetPaged(query, Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(25, result.PageSize);
    }

    [TestMethod]
    public async Task GetPaged_ShouldClampPageToMinimumOne()
    {
        // Arrange
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Company>(), 0);
        var query = new PagedQuery { Page = -1, PageSize = 25 };

        // Act
        var result = await _sut.GetPaged(query, Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.CurrentPage);
    }

    [TestMethod]
    public async Task GetPaged_ShouldAcceptValidPageSizes()
    {
        // Arrange
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Company>(), 0);
        var query = new PagedQuery { Page = 1, PageSize = 100 };

        // Act
        var result = await _sut.GetPaged(query, Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(100, result.PageSize);
    }

    [TestMethod]
    public async Task GetPaged_ShouldCalculateTotalPages()
    {
        // Arrange
        _repoMock.SetupGetPaged_Returns(new[] { Helpers.TestDataBuilders.CreateDefaultCompany() }, 75);

        // Act
        var result = await _sut.GetPaged(Helpers.TestDataBuilders.CreateDefaultPagedQuery(), Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(3, result.TotalPages);
    }

    // --- DeleteMany ---

    [TestMethod]
    public async Task DeleteMany_ShouldReturnDeletedCount()
    {
        // Arrange
        _repoMock.SetupDelete_Returns(true);
        var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = await _sut.DeleteMany(ids, CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public async Task DeleteMany_ShouldCountOnlySuccessful()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        _repoMock.Setup(r => r.Delete(id1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _repoMock.Setup(r => r.Delete(id2, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _sut.DeleteMany(new List<Guid> { id1, id2 }, CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result);
    }
}
