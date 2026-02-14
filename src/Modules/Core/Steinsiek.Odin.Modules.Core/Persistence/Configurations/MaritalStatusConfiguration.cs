namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="MaritalStatus"/>.
/// </summary>
public sealed class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MaritalStatus> builder)
    {
        builder.ToTable("MaritalStatuses", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new MaritalStatus
            {
                Id = Guid.Parse("00000030-0001-0001-0001-000000000001"),
                Code = "single",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new MaritalStatus
            {
                Id = Guid.Parse("00000030-0001-0001-0001-000000000002"),
                Code = "married",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new MaritalStatus
            {
                Id = Guid.Parse("00000030-0001-0001-0001-000000000003"),
                Code = "divorced",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
