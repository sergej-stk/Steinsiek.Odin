namespace Steinsiek.Odin.Modules.Auth.Tests.Helpers;

/// <summary>
/// Extension methods for fluent mock setup in Auth module tests.
/// </summary>
internal static class MockSetupExtensions
{
    /// <summary>
    /// Sets up <see cref="IUserRepository.GetByEmail"/> to return the given user.
    /// </summary>
    public static Mock<IUserRepository> SetupGetByEmail_Returns(this Mock<IUserRepository> mock, User? user)
    {
        mock.Setup(r => r.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IUserRepository.GetByEmail"/> for a specific email to return the given user.
    /// </summary>
    public static Mock<IUserRepository> SetupGetByEmail_Returns(this Mock<IUserRepository> mock, string email, User? user)
    {
        mock.Setup(r => r.GetByEmail(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IPasswordHasher.Verify"/> to return the given result.
    /// </summary>
    public static Mock<IPasswordHasher> SetupVerify_Returns(this Mock<IPasswordHasher> mock, bool result)
    {
        mock.Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(result);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IPasswordHasher.Hash"/> to return a fixed hash.
    /// </summary>
    public static Mock<IPasswordHasher> SetupHash_Returns(this Mock<IPasswordHasher> mock, string hash)
    {
        mock.Setup(h => h.Hash(It.IsAny<string>()))
            .Returns(hash);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IUserRepository.GetRoles"/> to return the given roles.
    /// </summary>
    public static Mock<IUserRepository> SetupGetRoles_Returns(this Mock<IUserRepository> mock, IReadOnlyList<string> roles)
    {
        mock.Setup(r => r.GetRoles(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(roles);
        return mock;
    }

    /// <summary>
    /// Sets up Add to return the entity that was passed in.
    /// </summary>
    public static Mock<IUserRepository> SetupAdd_ReturnsInput(this Mock<IUserRepository> mock)
    {
        mock.Setup(r => r.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User u, CancellationToken _) => u);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IUserRepository.AssignRole"/> to return the given result.
    /// </summary>
    public static Mock<IUserRepository> SetupAssignRole_Returns(this Mock<IUserRepository> mock, bool result)
    {
        mock.Setup(r => r.AssignRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        return mock;
    }

    /// <summary>
    /// Sets up <see cref="IUserRepository.RemoveRole"/> to return the given result.
    /// </summary>
    public static Mock<IUserRepository> SetupRemoveRole_Returns(this Mock<IUserRepository> mock, bool result)
    {
        mock.Setup(r => r.RemoveRole(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);
        return mock;
    }
}
