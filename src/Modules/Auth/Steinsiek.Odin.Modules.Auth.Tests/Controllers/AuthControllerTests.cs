namespace Steinsiek.Odin.Modules.Auth.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="AuthController"/>.
/// </summary>
[TestClass]
public sealed class AuthControllerTests
{
    private Mock<IAuthService> _serviceMock = null!;
    private AuthController _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IAuthService>();
        _sut = new AuthController(_serviceMock.Object);
    }

    [TestMethod]
    public async Task Login_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var response = new LoginResponse
        {
            Token = "jwt-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = Guid.NewGuid(),
                Email = "demo@steinsiek.de",
                FirstName = "Demo",
                LastName = "User",
                Roles = [OdinRoles.Admin]
            }
        };
        _serviceMock.Setup(s => s.Login(It.IsAny<LoginRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual("jwt-token", result.Value.Token);
    }

    [TestMethod]
    public async Task Login_ShouldReturnUnauthorized_WhenFails()
    {
        // Arrange
        _serviceMock.Setup(s => s.Login(It.IsAny<LoginRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginResponse?)null);

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<UnauthorizedObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task Register_ShouldReturnCreated_WhenSuccessful()
    {
        // Arrange
        var response = new LoginResponse
        {
            Token = "jwt-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = Guid.NewGuid(),
                Email = "new@steinsiek.de",
                FirstName = "New",
                LastName = "User"
            }
        };
        _serviceMock.Setup(s => s.Register(It.IsAny<RegisterRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.Register(Helpers.TestDataBuilders.CreateDefaultRegisterRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<CreatedAtActionResult>(result.Result);
    }

    [TestMethod]
    public async Task Register_ShouldReturnBadRequest_WhenEmailExists()
    {
        // Arrange
        _serviceMock.Setup(s => s.Register(It.IsAny<RegisterRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginResponse?)null);

        // Act
        var result = await _sut.Register(Helpers.TestDataBuilders.CreateDefaultRegisterRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task GetCurrentUser_ShouldReturnUserDto_WhenClaimsPresent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, "demo@steinsiek.de"),
            new(ClaimTypes.GivenName, "Demo"),
            new(ClaimTypes.Surname, "User"),
            new(ClaimTypes.Role, OdinRoles.Admin)
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);
        _sut.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        // Act
        var result = await _sut.GetCurrentUser(CancellationToken.None);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(userId, result.Value.Id);
        Assert.AreEqual("demo@steinsiek.de", result.Value.Email);
        Assert.AreEqual("Demo", result.Value.FirstName);
    }

    [TestMethod]
    public async Task GetCurrentUser_ShouldReturnUnauthorized_WhenNoSubjectClaim()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, "demo@steinsiek.de")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);
        _sut.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        // Act
        var result = await _sut.GetCurrentUser(CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<UnauthorizedResult>(result.Result);
    }

    [TestMethod]
    public async Task GetRoles_ShouldReturnOk_WithRoleList()
    {
        // Arrange
        var roles = new List<RoleDto>
        {
            Helpers.TestDataBuilders.CreateDefaultRoleDto()
        };
        _serviceMock.Setup(s => s.GetAllRoles(It.IsAny<CancellationToken>()))
            .ReturnsAsync(roles);

        // Act
        var result = await _sut.GetRoles(CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [TestMethod]
    public async Task AssignRole_ShouldReturnNoContent_WhenSuccessful()
    {
        // Arrange
        _serviceMock.Setup(s => s.AssignRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.AssignRole(Guid.NewGuid(), Helpers.TestDataBuilders.CreateDefaultAssignRoleRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
    }

    [TestMethod]
    public async Task AssignRole_ShouldReturnBadRequest_WhenAlreadyAssigned()
    {
        // Arrange
        _serviceMock.Setup(s => s.AssignRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.AssignRole(Guid.NewGuid(), Helpers.TestDataBuilders.CreateDefaultAssignRoleRequest(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(result);
    }

    [TestMethod]
    public async Task RemoveRole_ShouldReturnNoContent_WhenSuccessful()
    {
        // Arrange
        _serviceMock.Setup(s => s.RemoveRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.RemoveRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
    }

    [TestMethod]
    public async Task RemoveRole_ShouldReturnNotFound_WhenNotAssigned()
    {
        // Arrange
        _serviceMock.Setup(s => s.RemoveRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _sut.RemoveRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(result);
    }
}
