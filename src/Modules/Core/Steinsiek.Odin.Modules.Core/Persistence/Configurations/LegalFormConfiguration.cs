namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="LegalForm"/>.
/// </summary>
public sealed class LegalFormConfiguration : IEntityTypeConfiguration<LegalForm>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LegalForm> builder)
    {
        builder.ToTable("LegalForms", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new LegalForm
            {
                Id = Guid.Parse("00000080-0001-0001-0001-000000000001"),
                Code = "gmbh",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new LegalForm
            {
                Id = Guid.Parse("00000080-0001-0001-0001-000000000002"),
                Code = "ag",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new LegalForm
            {
                Id = Guid.Parse("00000080-0001-0001-0001-000000000003"),
                Code = "ug",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new LegalForm
            {
                Id = Guid.Parse("00000080-0001-0001-0001-000000000004"),
                Code = "ltd",
                SortOrder = 4,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new LegalForm
            {
                Id = Guid.Parse("00000080-0001-0001-0001-000000000005"),
                Code = "einzelunternehmen",
                SortOrder = 5,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
