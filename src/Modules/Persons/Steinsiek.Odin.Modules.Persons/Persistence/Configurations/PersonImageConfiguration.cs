namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonImage"/> entity.
/// </summary>
public sealed class PersonImageConfiguration : IEntityTypeConfiguration<PersonImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonImage> builder)
    {
        builder.ToTable("PersonImages", OdinSchemas.Persons);

        builder.HasKey(i => i.PersonId);

        builder.Property(i => i.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(i => i.Data)
            .IsRequired();
    }
}
