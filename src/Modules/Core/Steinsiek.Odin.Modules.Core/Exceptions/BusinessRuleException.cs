namespace Steinsiek.Odin.Modules.Core.Exceptions;

/// <summary>
/// Exception thrown when a business rule is violated.
/// </summary>
public sealed class BusinessRuleException : OdinException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleException"/> class.
    /// </summary>
    /// <param name="message">The business rule violation message.</param>
    public BusinessRuleException(string message)
        : base(message, 400, OdinErrorType.BusinessRule)
    {
    }
}
