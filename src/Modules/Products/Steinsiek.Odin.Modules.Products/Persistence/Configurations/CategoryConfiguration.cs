namespace Steinsiek.Odin.Modules.Products.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Category"/> entity.
/// </summary>
public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private static readonly Guid ElectronicsId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid ClothingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid BooksId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid HouseholdId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

    private static readonly DateTime SeedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
            new Category { Id = ElectronicsId, Name = "Electronics", Description = "Smartphones, laptops and more", CreatedAt = SeedDate },
            new Category { Id = ClothingId, Name = "Clothing", Description = "Fashion for every occasion", CreatedAt = SeedDate },
            new Category { Id = BooksId, Name = "Books", Description = "Non-fiction and fiction", CreatedAt = SeedDate },
            new Category { Id = HouseholdId, Name = "Household", Description = "Everything for your home", CreatedAt = SeedDate }
        );
    }
}
