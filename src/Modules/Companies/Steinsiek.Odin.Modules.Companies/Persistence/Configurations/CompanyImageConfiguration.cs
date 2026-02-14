namespace Steinsiek.Odin.Modules.Companies.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="CompanyImage"/> entity.
/// </summary>
public sealed class CompanyImageConfiguration : IEntityTypeConfiguration<CompanyImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<CompanyImage> builder)
    {
        builder.ToTable("CompanyImages", "companies");

        builder.HasKey(ci => ci.CompanyId);

        builder.Property(ci => ci.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ci => ci.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(ci => ci.Data)
            .IsRequired();

        builder.HasOne<Company>()
            .WithOne(c => c.Image)
            .HasForeignKey<CompanyImage>(ci => ci.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
