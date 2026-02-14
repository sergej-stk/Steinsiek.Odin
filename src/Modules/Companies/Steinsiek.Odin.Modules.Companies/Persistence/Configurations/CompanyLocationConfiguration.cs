namespace Steinsiek.Odin.Modules.Companies.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="CompanyLocation"/> entity.
/// </summary>
public sealed class CompanyLocationConfiguration : IEntityTypeConfiguration<CompanyLocation>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<CompanyLocation> builder)
    {
        builder.ToTable("CompanyLocations", OdinSchemas.Companies);

        builder.HasKey(cl => cl.Id);

        builder.Property(cl => cl.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(cl => cl.Street)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(cl => cl.Street2)
            .HasMaxLength(256);

        builder.Property(cl => cl.City)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(cl => cl.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(cl => cl.State)
            .HasMaxLength(200);

        builder.Property(cl => cl.Phone)
            .HasMaxLength(256);

        builder.Property(cl => cl.Email)
            .HasMaxLength(256);

        builder.HasOne<Company>()
            .WithMany(c => c.Locations)
            .HasForeignKey(cl => cl.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new CompanyLocation
            {
                Id = Guid.Parse("33333333-0002-0001-0001-000000000001"),
                CompanyId = Guid.Parse("33333333-0001-0001-0001-000000000001"),
                Name = "Headquarters",
                Street = "Musterstra\u00dfe 1",
                City = "Hamburg",
                PostalCode = "20095",
                CountryId = Guid.Parse("00000040-0001-0001-0001-000000000001"),
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new CompanyLocation
            {
                Id = Guid.Parse("33333333-0002-0001-0001-000000000002"),
                CompanyId = Guid.Parse("33333333-0001-0001-0001-000000000002"),
                Name = "Main Office",
                Street = "10 Downing Street",
                City = "London",
                PostalCode = "SW1A 2AA",
                CountryId = Guid.Parse("00000040-0001-0001-0001-000000000004"),
                IsPrimary = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
