namespace Steinsiek.Odin.Modules.Core.Exceptions;

/// <summary>
/// Exception thrown when input validation fails.
/// </summary>
public sealed class OdinValidationException : OdinException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OdinValidationException"/> class.
    /// </summary>
    /// <param name="message">The validation error message.</param>
    public OdinValidationException(string message)
        : base(message, 400, OdinErrorType.Validation)
    {
    }
}
