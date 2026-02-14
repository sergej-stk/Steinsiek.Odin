namespace Steinsiek.Odin.Modules.Auth.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="User"/> entity.
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Pre-computed BCrypt hash for the demo user password "Demo123!".
    /// </summary>
    private const string DemoPasswordHash = "$2a$11$.eVWCuc9QF3UHNOeQX9sAuDmkRj2MYyZvTFbsLjlfvvz7711qoFOK";

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", OdinSchemas.Auth);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Ignore(u => u.FullName);

        builder.Property(u => u.PreferredLanguage)
            .IsRequired()
            .HasMaxLength(10)
            .HasDefaultValue("en");

        builder.HasData(new User
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Email = "demo@steinsiek.de",
            PasswordHash = DemoPasswordHash,
            FirstName = "Demo",
            LastName = "User",
            IsActive = true,
            PreferredLanguage = "en",
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
