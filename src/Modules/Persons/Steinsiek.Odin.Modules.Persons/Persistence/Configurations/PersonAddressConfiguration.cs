namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonAddress"/> entity.
/// </summary>
public sealed class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonAddress> builder)
    {
        builder.ToTable("PersonAddresses", OdinSchemas.Persons);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Street2)
            .HasMaxLength(200);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.State)
            .HasMaxLength(100);

        builder.HasData(
            new PersonAddress
            {
                Id = Guid.Parse("33333333-0001-0001-0001-000000000001"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000001"),
                Street = "Musterstrasse 1",
                City = "Berlin",
                PostalCode = "10115",
                State = "Berlin",
                CountryId = Guid.Parse("00000040-0001-0001-0001-000000000001"),
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PersonAddress
            {
                Id = Guid.Parse("33333333-0001-0001-0001-000000000002"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000002"),
                Street = "123 Main Street",
                Street2 = "Apt 4B",
                City = "New York",
                PostalCode = "10001",
                State = "NY",
                CountryId = Guid.Parse("00000040-0001-0001-0001-000000000004"),
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
