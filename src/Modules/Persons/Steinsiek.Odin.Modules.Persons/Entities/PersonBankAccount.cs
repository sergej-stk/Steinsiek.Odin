namespace Steinsiek.Odin.Modules.Persons.Entities;

/// <summary>
/// Represents a bank account associated with a person.
/// </summary>
public class PersonBankAccount : BaseEntity
{
    /// <summary>
    /// The identifier of the person this bank account belongs to.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The International Bank Account Number (IBAN).
    /// </summary>
    public required string Iban { get; set; }

    /// <summary>
    /// The optional Bank Identifier Code (BIC/SWIFT).
    /// </summary>
    public string? Bic { get; set; }

    /// <summary>
    /// The optional name of the bank.
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// An optional descriptive label (e.g., "Primary", "Savings").
    /// </summary>
    public string? Label { get; set; }
}
