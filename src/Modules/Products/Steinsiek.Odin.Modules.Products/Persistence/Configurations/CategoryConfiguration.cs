namespace Steinsiek.Odin.Modules.Products.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Category"/> entity.
/// </summary>
public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private static readonly Guid _electronicsId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid _clothingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid _booksId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid _householdId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

    private static readonly DateTime _seedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Category { Id = _electronicsId, Name = "Electronics", Description = "Smartphones, laptops and more", CreatedAt = _seedDate },
            new Category { Id = _clothingId, Name = "Clothing", Description = "Fashion for every occasion", CreatedAt = _seedDate },
            new Category { Id = _booksId, Name = "Books", Description = "Non-fiction and fiction", CreatedAt = _seedDate },
            new Category { Id = _householdId, Name = "Household", Description = "Everything for your home", CreatedAt = _seedDate }
        );
    }
}
