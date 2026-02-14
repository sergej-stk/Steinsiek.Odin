namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonLanguage"/> entity.
/// </summary>
public sealed class PersonLanguageConfiguration : IEntityTypeConfiguration<PersonLanguage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonLanguage> builder)
    {
        builder.ToTable("PersonLanguages", OdinSchemas.Persons);

        builder.HasKey(l => l.Id);

        builder.Property(l => l.ProficiencyLevel)
            .HasMaxLength(50);
    }
}
