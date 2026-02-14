namespace Steinsiek.Odin.Modules.Products.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="Product"/> entity.
/// </summary>
public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private static readonly Guid _electronicsId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid _clothingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid _booksId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid _householdId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

    private static readonly DateTime _seedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000001"),
                Name = "iPhone 15 Pro",
                Description = "The latest iPhone with titanium case",
                Price = 1199.00m,
                Stock = 50,
                CategoryId = _electronicsId,
                ImageUrl = "/images/products/iphone-15-pro.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000002"),
                Name = "MacBook Air M3",
                Description = "Lightweight notebook with Apple M3 chip",
                Price = 1299.00m,
                Stock = 30,
                CategoryId = _electronicsId,
                ImageUrl = "/images/products/macbook-air-m3.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000003"),
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Flagship smartphone with S Pen",
                Price = 1449.00m,
                Stock = 25,
                CategoryId = _electronicsId,
                ImageUrl = "/images/products/galaxy-s24-ultra.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000004"),
                Name = "Premium Hoodie",
                Description = "Comfortable hoodie made of organic cotton",
                Price = 79.99m,
                Stock = 100,
                CategoryId = _clothingId,
                ImageUrl = "/images/products/premium-hoodie.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000005"),
                Name = "Designer Jeans",
                Description = "High-quality denim jeans",
                Price = 129.99m,
                Stock = 75,
                CategoryId = _clothingId,
                ImageUrl = "/images/products/designer-jeans.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000006"),
                Name = "Clean Code",
                Description = "Robert C. Martin - A Handbook of Agile Software Craftsmanship",
                Price = 39.99m,
                Stock = 200,
                CategoryId = _booksId,
                ImageUrl = "/images/products/clean-code.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000007"),
                Name = "Design Patterns",
                Description = "Gang of Four - Elements of Reusable Object-Oriented Software",
                Price = 49.99m,
                Stock = 150,
                CategoryId = _booksId,
                ImageUrl = "/images/products/design-patterns.jpg",
                CreatedAt = _seedDate
            },
            new Product
            {
                Id = Guid.Parse("11111111-0001-0001-0001-000000000008"),
                Name = "Coffee Machine Deluxe",
                Description = "Fully automatic espresso machine",
                Price = 599.00m,
                Stock = 20,
                CategoryId = _householdId,
                ImageUrl = "/images/products/coffee-machine-deluxe.jpg",
                CreatedAt = _seedDate
            }
        );
    }
}
