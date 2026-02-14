namespace Steinsiek.Odin.Modules.Auth.Tests.Helpers;

/// <summary>
/// Provides factory methods for creating test data in Auth module tests.
/// </summary>
internal static class TestDataBuilders
{
    /// <summary>
    /// Well-known user identifier for test data.
    /// </summary>
    public static readonly Guid DefaultUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    /// <summary>
    /// Well-known role identifier for test data.
    /// </summary>
    public static readonly Guid DefaultRoleId = Guid.Parse("aaaa0001-0001-0001-0001-000000000001");

    /// <summary>
    /// Pre-computed BCrypt hash for "Demo123!" used in test data.
    /// </summary>
    public const string DefaultPasswordHash = "$2a$11$rICvMJCdGe1SvMhsamNvJeEiMfPQcTaHzPDjWdq8fTLPBdfGmjYWO";

    /// <summary>
    /// Creates a default active user with known credentials.
    /// </summary>
    public static User CreateDefaultUser()
    {
        return new User
        {
            Id = DefaultUserId,
            Email = "demo@steinsiek.de",
            PasswordHash = DefaultPasswordHash,
            FirstName = "Demo",
            LastName = "User",
            IsActive = true,
            PreferredLanguage = "en",
            UserRoles = []
        };
    }

    /// <summary>
    /// Creates an inactive user for testing login failure scenarios.
    /// </summary>
    public static User CreateInactiveUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = "inactive@steinsiek.de",
            PasswordHash = DefaultPasswordHash,
            FirstName = "Inactive",
            LastName = "User",
            IsActive = false,
            PreferredLanguage = "en",
            UserRoles = []
        };
    }

    /// <summary>
    /// Creates a default login request with valid credentials.
    /// </summary>
    public static LoginRequest CreateDefaultLoginRequest()
    {
        return new LoginRequest
        {
            Email = "demo@steinsiek.de",
            Password = "Demo123!"
        };
    }

    /// <summary>
    /// Creates a default registration request.
    /// </summary>
    public static RegisterRequest CreateDefaultRegisterRequest()
    {
        return new RegisterRequest
        {
            Email = "new@steinsiek.de",
            Password = "NewPass123!",
            FirstName = "New",
            LastName = "User"
        };
    }

    /// <summary>
    /// Creates a test JWT configuration with all required keys.
    /// </summary>
    public static IConfiguration CreateTestJwtConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ConfigKeys.Jwt.Key] = "ThisIsAVeryLongSecretKeyForTestingPurposesOnly12345",
                [ConfigKeys.Jwt.Issuer] = "TestIssuer",
                [ConfigKeys.Jwt.Audience] = "TestAudience",
                [ConfigKeys.Jwt.ExpirationHours] = "1"
            })
            .Build();
    }

    /// <summary>
    /// Creates a default role DTO.
    /// </summary>
    public static RoleDto CreateDefaultRoleDto()
    {
        return new RoleDto
        {
            Id = DefaultRoleId,
            Name = OdinRoles.Admin,
            Description = "Full access"
        };
    }

    /// <summary>
    /// Creates a default assign role request.
    /// </summary>
    public static AssignRoleRequest CreateDefaultAssignRoleRequest()
    {
        return new AssignRoleRequest
        {
            RoleId = DefaultRoleId
        };
    }
}
