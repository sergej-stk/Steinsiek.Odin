namespace Steinsiek.Odin.Modules.Persons.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="PersonsController"/>.
/// </summary>
[TestClass]
public sealed class PersonsControllerTests
{
    private Mock<IPersonService> _serviceMock = null!;
    private Mock<ILogger<PersonsController>> _loggerMock = null!;
    private PersonsController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IPersonService>();
        _loggerMock = new Mock<ILogger<PersonsController>>();
        _sut = new PersonsController(_serviceMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnOk_WithPersons()
    {
        // Arrange
        var expected = new ListResult<PersonDto>
        {
            TotalCount = 1,
            Data = [new PersonDto { Id = Guid.NewGuid(), FirstName = "Max", LastName = "Mustermann" }]
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
        var expected = new PagedResult<PersonDto>
        {
            TotalCount = 1,
            Data = [new PersonDto { Id = Guid.NewGuid(), FirstName = "Max", LastName = "Mustermann" }],
            CurrentPage = 1,
            PageSize = 25
        };
        _serviceMock.Setup(s => s.GetPaged(It.IsAny<PagedQuery>(), It.IsAny<PersonFilterQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _sut.GetPaged(new PagedQuery(), new PersonFilterQuery(), CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
    }

    [TestMethod]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        // Arrange
        var detail = new PersonDetailDto
        {
            Id = Guid.NewGuid(), FirstName = "Max", LastName = "Mustermann",
            FullName = "Max Mustermann", CreatedAt = DateTime.UtcNow
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
            .ReturnsAsync((PersonDetailDto?)null);

        // Act
        var result = await _sut.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(result.Result);
    }

    [TestMethod]
    public async Task Search_ShouldReturnOk_WithMatchingPersons()
    {
        // Arrange
        var expected = new ListResult<PersonDto>
        {
            TotalCount = 1,
            Data = [new PersonDto { Id = Guid.NewGuid(), FirstName = "Max", LastName = "Mustermann" }]
        };
        _serviceMock.Setup(s => s.Search("Max", It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var result = await _sut.Search("Max", CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
    }

    [TestMethod]
    public async Task Search_ShouldCallGetAll_WhenQueryIsEmpty()
    {
        // Arrange
        var expected = new ListResult<PersonDto> { TotalCount = 0, Data = [] };
        _serviceMock.Setup(s => s.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        await _sut.Search("", CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetAll(It.IsAny<CancellationToken>()), Times.Once);
        _serviceMock.Verify(s => s.Search(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Search_ShouldCallGetAll_WhenQueryIsNull()
    {
        // Arrange
        var expected = new ListResult<PersonDto> { TotalCount = 0, Data = [] };
        _serviceMock.Setup(s => s.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        await _sut.Search(null, CancellationToken.None);

        // Assert
        _serviceMock.Verify(s => s.GetAll(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new PersonDto { Id = Guid.NewGuid(), FirstName = "New", LastName = "Person" };
        _serviceMock.Setup(s => s.Create(It.IsAny<CreatePersonRequest>(), It.IsAny<CancellationToken>()))
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
        var dto = new PersonDto { Id = Guid.NewGuid(), FirstName = "Updated", LastName = "Person" };
        _serviceMock.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<UpdatePersonRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        // Act
        var result = await _sut.Update(dto.Id, Helpers.TestDataBuilders.CreateDefaultUpdateRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual("Updated", result.Value.FirstName);
    }

    [TestMethod]
    public async Task Update_ShouldReturnNotFound_WhenNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<UpdatePersonRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PersonDto?)null);

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
            .ReturnsAsync(3);

        // Act
        var result = await _sut.DeleteMany(new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }, CancellationToken.None);

        // Assert
        Assert.AreEqual(3, result.Value);
    }
}
