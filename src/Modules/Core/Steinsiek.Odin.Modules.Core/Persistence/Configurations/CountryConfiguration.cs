namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Country"/>.
/// </summary>
public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Country
            {
                Id = Guid.Parse("00000040-0001-0001-0001-000000000001"),
                Code = "DE",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Country
            {
                Id = Guid.Parse("00000040-0001-0001-0001-000000000002"),
                Code = "AT",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Country
            {
                Id = Guid.Parse("00000040-0001-0001-0001-000000000003"),
                Code = "CH",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Country
            {
                Id = Guid.Parse("00000040-0001-0001-0001-000000000004"),
                Code = "GB",
                SortOrder = 4,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Country
            {
                Id = Guid.Parse("00000040-0001-0001-0001-000000000005"),
                Code = "US",
                SortOrder = 5,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
