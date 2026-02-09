namespace Steinsiek.Odin.Modules.Core.Exceptions;

/// <summary>
/// Base exception for all Odin domain exceptions. Carries an HTTP status code and error type.
/// </summary>
public class OdinException : Exception
{
    /// <summary>
    /// The HTTP status code associated with this exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// The error type classification.
    /// </summary>
    public OdinErrorType ErrorType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OdinException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="errorType">The error type classification.</param>
    public OdinException(string message, int statusCode, OdinErrorType errorType)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorType = errorType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OdinException"/> class with an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="errorType">The error type classification.</param>
    /// <param name="innerException">The inner exception.</param>
    public OdinException(string message, int statusCode, OdinErrorType errorType, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorType = errorType;
    }
}
