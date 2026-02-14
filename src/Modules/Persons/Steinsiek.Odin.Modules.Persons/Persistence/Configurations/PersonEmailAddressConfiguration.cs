namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonEmailAddress"/> entity.
/// </summary>
public sealed class PersonEmailAddressConfiguration : IEntityTypeConfiguration<PersonEmailAddress>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonEmailAddress> builder)
    {
        builder.ToTable("PersonEmailAddresses", "persons");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.Label)
            .HasMaxLength(50);

        builder.HasData(
            new PersonEmailAddress
            {
                Id = Guid.Parse("44444444-0001-0001-0001-000000000001"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000001"),
                Email = "max@mustermann.de",
                Label = "Personal",
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PersonEmailAddress
            {
                Id = Guid.Parse("44444444-0001-0001-0001-000000000002"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000002"),
                Email = "jane@doe.com",
                Label = "Personal",
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
