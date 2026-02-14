namespace Steinsiek.Odin.Modules.Products.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="ProductImageData"/> entity.
/// </summary>
public sealed class ProductImageDataConfiguration : IEntityTypeConfiguration<ProductImageData>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProductImageData> builder)
    {
        builder.ToTable("ProductImages", "products");

        builder.HasKey(i => i.ProductId);

        builder.Property(i => i.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(i => i.Data)
            .IsRequired();

        builder.HasOne(i => i.Product)
            .WithOne()
            .HasForeignKey<ProductImageData>(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
