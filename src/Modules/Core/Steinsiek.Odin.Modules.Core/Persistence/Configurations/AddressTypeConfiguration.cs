namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="AddressType"/>.
/// </summary>
public sealed class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AddressType> builder)
    {
        builder.ToTable("AddressTypes", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new AddressType
            {
                Id = Guid.Parse("00000050-0001-0001-0001-000000000001"),
                Code = "private",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new AddressType
            {
                Id = Guid.Parse("00000050-0001-0001-0001-000000000002"),
                Code = "business",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new AddressType
            {
                Id = Guid.Parse("00000050-0001-0001-0001-000000000003"),
                Code = "delivery",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
