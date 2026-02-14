namespace Steinsiek.Odin.Modules.Auth.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Role"/> entity.
/// </summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Well-known identifier for the Admin role.
    /// </summary>
    public static readonly Guid AdminRoleId = Guid.Parse("aaaa0001-0001-0001-0001-000000000001");

    /// <summary>
    /// Well-known identifier for the Manager role.
    /// </summary>
    public static readonly Guid ManagerRoleId = Guid.Parse("aaaa0001-0001-0001-0001-000000000002");

    /// <summary>
    /// Well-known identifier for the User role.
    /// </summary>
    public static readonly Guid UserRoleId = Guid.Parse("aaaa0001-0001-0001-0001-000000000003");

    /// <summary>
    /// Well-known identifier for the ReadOnly role.
    /// </summary>
    public static readonly Guid ReadOnlyRoleId = Guid.Parse("aaaa0001-0001-0001-0001-000000000004");

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "auth");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name)
            .IsUnique();

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.HasData(
            new Role
            {
                Id = AdminRoleId,
                Name = "Admin",
                Description = "Full access including audit log, user management and roles",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = ManagerRoleId,
                Name = "Manager",
                Description = "CRUD access to persons and companies",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = UserRoleId,
                Name = "User",
                Description = "Limited CRUD access to persons and companies",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Role
            {
                Id = ReadOnlyRoleId,
                Name = "ReadOnly",
                Description = "Read-only access to all data",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
