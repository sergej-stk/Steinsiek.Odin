namespace Steinsiek.Odin.Modules.Persons.Persistence.Configurations;

/// <summary>
/// Entity type configuration for the <see cref="PersonBankAccount"/> entity.
/// </summary>
public sealed class PersonBankAccountConfiguration : IEntityTypeConfiguration<PersonBankAccount>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PersonBankAccount> builder)
    {
        builder.ToTable("PersonBankAccounts", "persons");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Iban)
            .IsRequired()
            .HasMaxLength(34);

        builder.Property(b => b.Bic)
            .HasMaxLength(11);

        builder.Property(b => b.BankName)
            .HasMaxLength(200);

        builder.Property(b => b.Label)
            .HasMaxLength(50);
    }
}
