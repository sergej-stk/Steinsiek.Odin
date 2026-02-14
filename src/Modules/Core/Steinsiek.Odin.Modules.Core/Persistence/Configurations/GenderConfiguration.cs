namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Gender"/>.
/// </summary>
public sealed class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.ToTable("Genders", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Gender
            {
                Id = Guid.Parse("00000020-0001-0001-0001-000000000001"),
                Code = "male",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Gender
            {
                Id = Guid.Parse("00000020-0001-0001-0001-000000000002"),
                Code = "female",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Gender
            {
                Id = Guid.Parse("00000020-0001-0001-0001-000000000003"),
                Code = "non_binary",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
