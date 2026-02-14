namespace Steinsiek.Odin.Modules.Auth.Repositories;

/// <summary>
/// Entity Framework Core implementation of the user repository.
/// </summary>
public sealed class EfUserRepository(OdinDbContext context) : IUserRepository
{
    private readonly OdinDbContext _context = context;

    /// <inheritdoc />
    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<User>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var normalizedEmail = email.ToLower(System.Globalization.CultureInfo.InvariantCulture);
        return await _context.Set<User>()
#pragma warning disable RCS1155 // ToLower is required for EF Core LINQ-to-SQL translation
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
#pragma warning restore RCS1155
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<User>().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User> Add(User entity, CancellationToken cancellationToken)
    {
        _context.Set<User>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<User> Update(User entity, CancellationToken cancellationToken)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<User>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<User>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Set<User>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<string>> GetRoles(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role!.Name)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var exists = await _context.Set<UserRole>()
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (exists)
        {
            return false;
        }

        _context.Set<UserRole>().Add(new UserRole { UserId = userId, RoleId = roleId });
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        var userRole = await _context.Set<UserRole>()
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (userRole is null)
        {
            return false;
        }

        _context.Set<UserRole>().Remove(userRole);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
