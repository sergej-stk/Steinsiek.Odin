namespace Steinsiek.Odin.Modules.Auth.Tests.Services;

/// <summary>
/// Unit tests for <see cref="BcryptPasswordHasher"/>.
/// </summary>
[TestClass]
public sealed class BcryptPasswordHasherTests
{
    private BcryptPasswordHasher _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _sut = new BcryptPasswordHasher();
    }

    [TestMethod]
    public void Hash_ShouldReturnBcryptHash()
    {
        // Act
        var hash = _sut.Hash("TestPassword123!");

        // Assert
        Assert.IsNotNull(hash);
        StringAssert.StartsWith(hash, "$2");
    }

    [TestMethod]
    public void Hash_ShouldReturnDifferentHashesForSameInput()
    {
        // Act
        var hash1 = _sut.Hash("TestPassword123!");
        var hash2 = _sut.Hash("TestPassword123!");

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }

    [TestMethod]
    public void Verify_ShouldReturnTrue_WhenPasswordMatches()
    {
        // Arrange
        var password = "TestPassword123!";
        var hash = _sut.Hash(password);

        // Act
        var result = _sut.Verify(password, hash);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Verify_ShouldReturnFalse_WhenPasswordDoesNotMatch()
    {
        // Arrange
        var hash = _sut.Hash("CorrectPassword");

        // Act
        var result = _sut.Verify("WrongPassword", hash);

        // Assert
        Assert.IsFalse(result);
    }
}
