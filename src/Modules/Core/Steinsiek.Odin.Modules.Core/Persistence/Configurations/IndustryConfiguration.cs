namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Industry"/>.
/// </summary>
public sealed class IndustryConfiguration : IEntityTypeConfiguration<Industry>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Industry> builder)
    {
        builder.ToTable("Industries", "core");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Industry
            {
                Id = Guid.Parse("00000070-0001-0001-0001-000000000001"),
                Code = "technology",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Industry
            {
                Id = Guid.Parse("00000070-0001-0001-0001-000000000002"),
                Code = "finance",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Industry
            {
                Id = Guid.Parse("00000070-0001-0001-0001-000000000003"),
                Code = "consulting",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Industry
            {
                Id = Guid.Parse("00000070-0001-0001-0001-000000000004"),
                Code = "manufacturing",
                SortOrder = 4,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Industry
            {
                Id = Guid.Parse("00000070-0001-0001-0001-000000000005"),
                Code = "healthcare",
                SortOrder = 5,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
