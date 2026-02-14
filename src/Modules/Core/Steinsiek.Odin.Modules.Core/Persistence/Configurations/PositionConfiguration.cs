namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Position"/>.
/// </summary>
public sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Position
            {
                Id = Guid.Parse("000000a0-0001-0001-0001-000000000001"),
                Code = "ceo",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Position
            {
                Id = Guid.Parse("000000a0-0001-0001-0001-000000000002"),
                Code = "manager",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Position
            {
                Id = Guid.Parse("000000a0-0001-0001-0001-000000000003"),
                Code = "developer",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Position
            {
                Id = Guid.Parse("000000a0-0001-0001-0001-000000000004"),
                Code = "consultant",
                SortOrder = 4,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Position
            {
                Id = Guid.Parse("000000a0-0001-0001-0001-000000000005"),
                Code = "assistant",
                SortOrder = 5,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
