namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="Department"/>.
/// </summary>
public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments", "core");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasData(
            new Department
            {
                Id = Guid.Parse("00000090-0001-0001-0001-000000000001"),
                Code = "sales",
                SortOrder = 1,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = Guid.Parse("00000090-0001-0001-0001-000000000002"),
                Code = "it",
                SortOrder = 2,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = Guid.Parse("00000090-0001-0001-0001-000000000003"),
                Code = "hr",
                SortOrder = 3,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = Guid.Parse("00000090-0001-0001-0001-000000000004"),
                Code = "finance_dept",
                SortOrder = 4,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Department
            {
                Id = Guid.Parse("00000090-0001-0001-0001-000000000005"),
                Code = "marketing",
                SortOrder = 5,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
    }
}
