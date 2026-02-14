namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Language"/>.
/// </summary>
public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Language
            {
                Id = Guid.Parse("00000001-0001-0001-0001-000000000001"),
                Code = "de",
                Name = "Deutsch",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Language
            {
                Id = Guid.Parse("00000001-0001-0001-0001-000000000002"),
                Code = "en",
                Name = "English",
                IsDefault = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
