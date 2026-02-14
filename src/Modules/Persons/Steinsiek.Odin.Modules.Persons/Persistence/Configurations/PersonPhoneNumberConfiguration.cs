namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonPhoneNumber"/> entity.
/// </summary>
public sealed class PersonPhoneNumberConfiguration : IEntityTypeConfiguration<PersonPhoneNumber>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonPhoneNumber> builder)
    {
        builder.ToTable("PersonPhoneNumbers", OdinSchemas.Persons);

        builder.HasKey(pn => pn.Id);

        builder.Property(pn => pn.Number)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(pn => pn.Label)
            .HasMaxLength(50);

        builder.HasData(
            new PersonPhoneNumber
            {
                Id = Guid.Parse("55555555-0001-0001-0001-000000000001"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000001"),
                Number = "+49 170 1234567",
                Label = "Mobile",
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PersonPhoneNumber
            {
                Id = Guid.Parse("55555555-0001-0001-0001-000000000002"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000002"),
                Number = "+1 212 5551234",
                Label = "Mobile",
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
