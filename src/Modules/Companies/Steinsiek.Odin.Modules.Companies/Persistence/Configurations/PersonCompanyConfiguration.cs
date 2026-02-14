namespace Steinsiek.Odin.Modules.Companies.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonCompany"/> junction entity.
/// </summary>
public sealed class PersonCompanyConfiguration : IEntityTypeConfiguration<PersonCompany>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonCompany> builder)
    {
        builder.ToTable("PersonCompanies", "companies");

        builder.HasKey(pc => pc.Id);

        builder.HasIndex(pc => new { pc.PersonId, pc.CompanyId })
            .IsUnique();

        builder.HasOne(pc => pc.Company)
            .WithMany(c => c.PersonCompanies)
            .HasForeignKey(pc => pc.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new PersonCompany
            {
                Id = Guid.Parse("33333333-0003-0001-0001-000000000001"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000001"),
                CompanyId = Guid.Parse("33333333-0001-0001-0001-000000000001"),
                PositionId = Guid.Parse("000000a0-0001-0001-0001-000000000001"),
                DepartmentId = Guid.Parse("00000090-0001-0001-0001-000000000002"),
                IsActive = true,
                StartDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PersonCompany
            {
                Id = Guid.Parse("33333333-0003-0001-0001-000000000002"),
                PersonId = Guid.Parse("22222222-0001-0001-0001-000000000002"),
                CompanyId = Guid.Parse("33333333-0001-0001-0001-000000000002"),
                PositionId = Guid.Parse("000000a0-0001-0001-0001-000000000002"),
                DepartmentId = Guid.Parse("00000090-0001-0001-0001-000000000001"),
                IsActive = true,
                StartDate = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
