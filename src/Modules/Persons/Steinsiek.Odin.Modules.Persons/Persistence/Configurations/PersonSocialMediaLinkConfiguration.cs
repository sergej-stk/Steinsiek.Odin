namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonSocialMediaLink"/> entity.
/// </summary>
public sealed class PersonSocialMediaLinkConfiguration : IEntityTypeConfiguration<PersonSocialMediaLink>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonSocialMediaLink> builder)
    {
        builder.ToTable("PersonSocialMediaLinks", "persons");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Platform)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Url)
            .IsRequired()
            .HasMaxLength(500);
    }
}
