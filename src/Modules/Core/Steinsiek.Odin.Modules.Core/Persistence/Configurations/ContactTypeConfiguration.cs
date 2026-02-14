namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="ContactType"/>.
/// </summary>
public sealed class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ContactType> builder)
    {
        builder.ToTable("ContactTypes", OdinSchemas.Core);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new ContactType
            {
                Id = Guid.Parse("00000060-0001-0001-0001-000000000001"),
                Code = "mobile",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ContactType
            {
                Id = Guid.Parse("00000060-0001-0001-0001-000000000002"),
                Code = "landline",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new ContactType
            {
                Id = Guid.Parse("00000060-0001-0001-0001-000000000003"),
                Code = "fax",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
