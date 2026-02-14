namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Person"/> entity.
/// </summary>
public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons", "persons");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Title)
            .HasMaxLength(50);

        builder.Property(p => p.Notes)
            .HasMaxLength(2000);

        builder.Ignore(p => p.FullName);

        builder.HasMany(p => p.Addresses)
            .WithOne()
            .HasForeignKey(a => a.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.EmailAddresses)
            .WithOne()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.PhoneNumbers)
            .WithOne()
            .HasForeignKey(pn => pn.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.BankAccounts)
            .WithOne()
            .HasForeignKey(b => b.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.SocialMediaLinks)
            .WithOne()
            .HasForeignKey(s => s.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Languages)
            .WithOne()
            .HasForeignKey(l => l.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Image)
            .WithOne()
            .HasForeignKey<PersonImage>(i => i.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Person
            {
                Id = Guid.Parse("22222222-0001-0001-0001-000000000001"),
                FirstName = "Max",
                LastName = "Mustermann",
                SalutationId = Guid.Parse("00000010-0001-0001-0001-000000000001"),
                GenderId = Guid.Parse("00000020-0001-0001-0001-000000000001"),
                NationalityId = Guid.Parse("00000040-0001-0001-0001-000000000001"),
                DateOfBirth = new DateTime(1990, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Person
            {
                Id = Guid.Parse("22222222-0001-0001-0001-000000000002"),
                FirstName = "Jane",
                LastName = "Doe",
                SalutationId = Guid.Parse("00000010-0001-0001-0001-000000000002"),
                GenderId = Guid.Parse("00000020-0001-0001-0001-000000000002"),
                NationalityId = Guid.Parse("00000040-0001-0001-0001-000000000004"),
                DateOfBirth = new DateTime(1985, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
