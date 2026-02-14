namespace Steinsiek.Odin.Modules.Persons.Tests.Services;

/// <summary>
/// Unit tests for <see cref="PersonService"/>.
/// </summary>
[TestClass]
public sealed class PersonServiceTests
{
    private Mock<IPersonRepository> _repoMock = null!;
    private Mock<ILogger<PersonService>> _loggerMock = null!;
    private PersonService _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _repoMock = new Mock<IPersonRepository>();
        _loggerMock = new Mock<ILogger<PersonService>>();
        _sut = new PersonService(_repoMock.Object, _loggerMock.Object);
    }

    // --- GetAll ---

    [TestMethod]
    public async Task GetAll_ShouldReturnAllPersons()
    {
        // Arrange
        var persons = new[] { Helpers.TestDataBuilders.CreateDefaultPerson(), Helpers.TestDataBuilders.CreateMinimalPerson() };
        _repoMock.SetupGetAll_Returns(persons);

        // Act
        var result = await _sut.GetAll(CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.TotalCount);
        Assert.HasCount(2, result.Data);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoPersons()
    {
        // Arrange
        _repoMock.SetupGetAll_Returns(Enumerable.Empty<Person>());

        // Act
        var result = await _sut.GetAll(CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.TotalCount);
    }

    // --- GetById ---

    [TestMethod]
    public async Task GetById_ShouldReturnDetailDto_WhenPersonExists()
    {
        // Arrange
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupGetById_Returns(person);

        // Act
        var result = await _sut.GetById(person.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(person.Id, result.Id);
        Assert.AreEqual(person.FirstName, result.FirstName);
        Assert.AreEqual(person.LastName, result.LastName);
        Assert.AreEqual(person.FullName, result.FullName);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnNull_WhenPersonNotFound()
    {
        // Arrange
        _repoMock.SetupGetById_Returns((Person?)null);

        // Act
        var result = await _sut.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetById_ShouldMapSubEntities()
    {
        // Arrange
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupGetById_Returns(person);

        // Act
        var result = await _sut.GetById(person.Id, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.HasCount(1, result.EmailAddresses);
        Assert.HasCount(1, result.PhoneNumbers);
        Assert.HasCount(1, result.Addresses);
    }

    // --- Create ---

    [TestMethod]
    public async Task Create_ShouldReturnCreatedPersonDto()
    {
        // Arrange
        _repoMock.SetupAdd_ReturnsInput();
        var request = Helpers.TestDataBuilders.CreateDefaultCreateRequest();

        // Act
        var result = await _sut.Create(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("New", result.FirstName);
        Assert.AreEqual("Person", result.LastName);
    }

    [TestMethod]
    public async Task Create_ShouldCallRepositoryAdd()
    {
        // Arrange
        _repoMock.SetupAdd_ReturnsInput();

        // Act
        await _sut.Create(Helpers.TestDataBuilders.CreateDefaultCreateRequest(), CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Add(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
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
            It.Is<Person>(p => p.FirstName == request.FirstName && p.LastName == request.LastName && p.Title == request.Title),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // --- Update ---

    [TestMethod]
    public async Task Update_ShouldReturnUpdatedDto_WhenPersonExists()
    {
        // Arrange
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupGetById_Returns(person);
        _repoMock.SetupUpdate_ReturnsInput();

        // Act
        var result = await _sut.Update(person.Id, Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Updated", result.FirstName);
    }

    [TestMethod]
    public async Task Update_ShouldReturnNull_WhenPersonNotFound()
    {
        // Arrange
        _repoMock.SetupGetById_Returns((Person?)null);

        // Act
        var result = await _sut.Update(Guid.NewGuid(), Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Update_ShouldApplyRequestFieldsToEntity()
    {
        // Arrange
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupGetById_Returns(person);
        _repoMock.SetupUpdate_ReturnsInput();
        var request = Helpers.TestDataBuilders.CreateDefaultUpdateRequest();

        // Act
        await _sut.Update(person.Id, request, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.Update(
            It.Is<Person>(p => p.FirstName == "Updated" && p.LastName == "Person"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // --- Delete ---

    [TestMethod]
    public async Task Delete_ShouldReturnTrue_WhenPersonExists()
    {
        // Arrange
        _repoMock.SetupDelete_Returns(true);

        // Act
        var result = await _sut.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Delete_ShouldReturnFalse_WhenPersonNotFound()
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
    public async Task Search_ShouldReturnMatchingPersons()
    {
        // Arrange
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupSearch_Returns(new[] { person });

        // Act
        var result = await _sut.Search("Max", CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.TotalCount);
        Assert.AreEqual("Max", result.Data[0].FirstName);
    }

    [TestMethod]
    public async Task Search_ShouldReturnEmptyList_WhenNoMatches()
    {
        // Arrange
        _repoMock.SetupSearch_Returns(Enumerable.Empty<Person>());

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
        var person = Helpers.TestDataBuilders.CreateDefaultPerson();
        _repoMock.SetupGetPaged_Returns(new[] { person }, 1);

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
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Person>(), 0);
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
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Person>(), 0);
        var query = new PagedQuery { Page = 0, PageSize = 25 };

        // Act
        var result = await _sut.GetPaged(query, Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(1, result.CurrentPage);
    }

    [TestMethod]
    public async Task GetPaged_ShouldAcceptValidPageSizes()
    {
        // Arrange
        _repoMock.SetupGetPaged_Returns(Enumerable.Empty<Person>(), 0);
        var query = new PagedQuery { Page = 1, PageSize = 50 };

        // Act
        var result = await _sut.GetPaged(query, Helpers.TestDataBuilders.CreateDefaultFilterQuery(), CancellationToken.None);

        // Assert
        Assert.AreEqual(50, result.PageSize);
    }

    [TestMethod]
    public async Task GetPaged_ShouldCalculateTotalPages()
    {
        // Arrange
        var persons = new[] { Helpers.TestDataBuilders.CreateDefaultPerson() };
        _repoMock.SetupGetPaged_Returns(persons, 75);

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
