namespace Steinsiek.Odin.Modules.Auth.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="UserRole"/> junction entity.
/// </summary>
public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles", OdinSchemas.Auth);

        builder.HasKey(ur => ur.Id);

        builder.HasIndex(ur => new { ur.UserId, ur.RoleId })
            .IsUnique();

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new UserRole
        {
            Id = Guid.Parse("aaaa0002-0001-0001-0001-000000000001"),
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            RoleId = RoleConfiguration.AdminRoleId,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
