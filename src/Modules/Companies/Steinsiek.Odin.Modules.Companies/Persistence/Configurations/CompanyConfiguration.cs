namespace Steinsiek.Odin.Modules.Companies.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Company"/> entity.
/// </summary>
public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies", "companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Website)
            .HasMaxLength(256);

        builder.Property(c => c.Email)
            .HasMaxLength(256);

        builder.Property(c => c.Phone)
            .HasMaxLength(256);

        builder.Property(c => c.TaxNumber)
            .HasMaxLength(100);

        builder.Property(c => c.VatId)
            .HasMaxLength(100);

        builder.Property(c => c.CommercialRegisterNumber)
            .HasMaxLength(100);

        builder.Property(c => c.Revenue)
            .HasPrecision(18, 2);

        builder.Property(c => c.Notes)
            .HasMaxLength(2000);

        builder.HasOne(c => c.ParentCompany)
            .WithMany(c => c.ChildCompanies)
            .HasForeignKey(c => c.ParentCompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Company
            {
                Id = Guid.Parse("33333333-0001-0001-0001-000000000001"),
                Name = "Steinsiek GmbH",
                LegalFormId = Guid.Parse("00000080-0001-0001-0001-000000000001"),
                IndustryId = Guid.Parse("00000070-0001-0001-0001-000000000001"),
                Website = "https://steinsiek.de",
                FoundingDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Company
            {
                Id = Guid.Parse("33333333-0001-0001-0001-000000000002"),
                Name = "Odin Consulting Ltd",
                LegalFormId = Guid.Parse("00000080-0001-0001-0001-000000000004"),
                IndustryId = Guid.Parse("00000070-0001-0001-0001-000000000003"),
                Website = "https://odin-consulting.com",
                FoundingDate = new DateTime(2018, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
