namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Salutation"/>.
/// </summary>
public sealed class SalutationConfiguration : IEntityTypeConfiguration<Salutation>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Salutation> builder)
    {
        builder.ToTable("Salutations", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Salutation
            {
                Id = Guid.Parse("00000010-0001-0001-0001-000000000001"),
                Code = "mr",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Salutation
            {
                Id = Guid.Parse("00000010-0001-0001-0001-000000000002"),
                Code = "mrs",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Salutation
            {
                Id = Guid.Parse("00000010-0001-0001-0001-000000000003"),
                Code = "mx",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
