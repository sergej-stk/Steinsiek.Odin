namespace Steinsiek.Odin.Modules.Auth.Tests.Services;

/// <summary>
/// Unit tests for <see cref="AuthService"/>.
/// </summary>
[TestClass]
public sealed class AuthServiceTests
{
    private Mock<IUserRepository> _userRepoMock = null!;
    private Mock<IPasswordHasher> _hasherMock = null!;
    private Mock<ILogger<AuthService>> _loggerMock = null!;
    private IConfiguration _configuration = null!;
    private AuthService _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _hasherMock = new Mock<IPasswordHasher>();
        _loggerMock = new Mock<ILogger<AuthService>>();
        _configuration = Helpers.TestDataBuilders.CreateTestJwtConfiguration();

        _sut = new AuthService(
            _userRepoMock.Object,
            _hasherMock.Object,
            _configuration,
            _loggerMock.Object);
    }

    [TestMethod]
    public async Task Login_ShouldReturnLoginResponse_WhenCredentialsValid()
    {
        // Arrange
        var user = Helpers.TestDataBuilders.CreateDefaultUser();
        _userRepoMock.SetupGetByEmail_Returns(user);
        _hasherMock.SetupVerify_Returns(true);
        _userRepoMock.SetupGetRoles_Returns(new List<string> { OdinRoles.Admin });

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Token);
        Assert.AreEqual(user.Email, result.User.Email);
        Assert.AreEqual(user.FirstName, result.User.FirstName);
    }

    [TestMethod]
    public async Task Login_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange
        _userRepoMock.SetupGetByEmail_Returns((User?)null);

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_ShouldReturnNull_WhenPasswordInvalid()
    {
        // Arrange
        var user = Helpers.TestDataBuilders.CreateDefaultUser();
        _userRepoMock.SetupGetByEmail_Returns(user);
        _hasherMock.SetupVerify_Returns(false);

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_ShouldReturnNull_WhenUserInactive()
    {
        // Arrange
        var user = Helpers.TestDataBuilders.CreateInactiveUser();
        _userRepoMock.SetupGetByEmail_Returns(user);
        _hasherMock.SetupVerify_Returns(true);

        // Act
        var request = new LoginRequest { Email = user.Email, Password = "Demo123!" };
        var result = await _sut.Login(request, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_ShouldGenerateValidJwtToken_WhenSuccessful()
    {
        // Arrange
        var user = Helpers.TestDataBuilders.CreateDefaultUser();
        _userRepoMock.SetupGetByEmail_Returns(user);
        _hasherMock.SetupVerify_Returns(true);
        _userRepoMock.SetupGetRoles_Returns(new List<string>());

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrWhiteSpace(result.Token));
        Assert.IsGreaterThan(DateTime.UtcNow, result.ExpiresAt);
    }

    [TestMethod]
    public async Task Login_ShouldIncludeRolesInToken_WhenUserHasRoles()
    {
        // Arrange
        var user = Helpers.TestDataBuilders.CreateDefaultUser();
        var roles = new List<string> { OdinRoles.Admin, OdinRoles.Manager };
        _userRepoMock.SetupGetByEmail_Returns(user);
        _hasherMock.SetupVerify_Returns(true);
        _userRepoMock.SetupGetRoles_Returns(roles);

        // Act
        var result = await _sut.Login(Helpers.TestDataBuilders.CreateDefaultLoginRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.HasCount(2, result.User.Roles);
        Assert.Contains(OdinRoles.Admin, result.User.Roles);
        Assert.Contains(OdinRoles.Manager, result.User.Roles);
    }

    [TestMethod]
    public async Task Register_ShouldReturnLoginResponse_WhenEmailNotTaken()
    {
        // Arrange
        _userRepoMock.SetupGetByEmail_Returns((User?)null);
        _hasherMock.SetupHash_Returns("$2a$11$hashed");
        _userRepoMock.SetupAdd_ReturnsInput();
        _userRepoMock.SetupGetRoles_Returns(new List<string>());

        // Act
        var result = await _sut.Register(Helpers.TestDataBuilders.CreateDefaultRegisterRequest(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Token);
        Assert.AreEqual("new@steinsiek.de", result.User.Email);
    }

    [TestMethod]
    public async Task Register_ShouldReturnNull_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingUser = Helpers.TestDataBuilders.CreateDefaultUser();
        _userRepoMock.Setup(r => r.GetByEmail("new@steinsiek.de", It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _sut.Register(Helpers.TestDataBuilders.CreateDefaultRegisterRequest(), CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Register_ShouldHashPassword_BeforeSaving()
    {
        // Arrange
        _userRepoMock.SetupGetByEmail_Returns((User?)null);
        _hasherMock.SetupHash_Returns("$2a$11$hashedpassword");
        _userRepoMock.SetupAdd_ReturnsInput();
        _userRepoMock.SetupGetRoles_Returns(new List<string>());

        // Act
        await _sut.Register(Helpers.TestDataBuilders.CreateDefaultRegisterRequest(), CancellationToken.None);

        // Assert
        _hasherMock.Verify(h => h.Hash("NewPass123!"), Times.Once);
        _userRepoMock.Verify(r => r.Add(
            It.Is<User>(u => u.PasswordHash == "$2a$11$hashedpassword"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [TestMethod]
    public async Task GetAllRoles_ShouldReturnAllRoles()
    {
        // Act
        var result = await _sut.GetAllRoles(CancellationToken.None);

        // Assert
        Assert.HasCount(4, result);
        Assert.IsTrue(result.Any(r => string.Equals(r.Name, OdinRoles.Admin, StringComparison.Ordinal)));
        Assert.IsTrue(result.Any(r => string.Equals(r.Name, OdinRoles.Manager, StringComparison.Ordinal)));
        Assert.IsTrue(result.Any(r => string.Equals(r.Name, OdinRoles.User, StringComparison.Ordinal)));
        Assert.IsTrue(result.Any(r => string.Equals(r.Name, OdinRoles.ReadOnly, StringComparison.Ordinal)));
    }

    [TestMethod]
    public async Task AssignRole_ShouldReturnTrue_WhenAssigned()
    {
        // Arrange
        _userRepoMock.SetupAssignRole_Returns(true);

        // Act
        var result = await _sut.AssignRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task AssignRole_ShouldReturnFalse_WhenAlreadyAssigned()
    {
        // Arrange
        _userRepoMock.SetupAssignRole_Returns(false);

        // Act
        var result = await _sut.AssignRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task RemoveRole_ShouldReturnTrue_WhenRemoved()
    {
        // Arrange
        _userRepoMock.SetupRemoveRole_Returns(true);

        // Act
        var result = await _sut.RemoveRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task RemoveRole_ShouldReturnFalse_WhenNotAssigned()
    {
        // Arrange
        _userRepoMock.SetupRemoveRole_Returns(false);

        // Act
        var result = await _sut.RemoveRole(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }
}
