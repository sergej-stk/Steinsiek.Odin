namespace Steinsiek.Odin.Modules.Persons.Shared.DTOs;

/// <summary>
/// Data transfer object for a person's bank account.
/// </summary>
public sealed record class PersonBankAccountDto
{
    /// <summary>
    /// The unique identifier of the bank account entry.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The International Bank Account Number (IBAN).
    /// </summary>
    public required string Iban { get; init; }

    /// <summary>
    /// The optional Bank Identifier Code (BIC/SWIFT).
    /// </summary>
    public string? Bic { get; init; }

    /// <summary>
    /// The optional name of the bank.
    /// </summary>
    public string? BankName { get; init; }

    /// <summary>
    /// An optional descriptive label.
    /// </summary>
    public string? Label { get; init; }
}
