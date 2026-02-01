namespace Steinsiek.Odin.Modules.Auth.Repositories;

/// <summary>
/// In-memory implementation of the user repository for development and testing.
/// </summary>
public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    /// <summary>
    /// Initializes a new instance and seeds demo data.
    /// </summary>
    public InMemoryUserRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var demoUser = new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Email = "demo@steinsiek.de",
            PasswordHash = HashPassword("Demo123!"),
            FirstName = "Demo",
            LastName = "User",
            CreatedAt = DateTime.UtcNow
        };
        _users.TryAdd(demoUser.Id, demoUser);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    /// <inheritdoc />
    public Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        _users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = _users.Values.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(user);
    }

    /// <inheritdoc />
    public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<User>>(_users.Values.ToList());
    }

    /// <inheritdoc />
    public Task<User> Add(User entity, CancellationToken cancellationToken)
    {
        entity.PasswordHash = HashPassword(entity.PasswordHash);
        _users.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<User> Update(User entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _users[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.TryRemove(id, out _));
    }

    /// <summary>
    /// Computes a SHA256 hash for the given password.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The base64-encoded hash.</returns>
    public static string ComputeHash(string password) => HashPassword(password);
}
