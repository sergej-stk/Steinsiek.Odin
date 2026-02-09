namespace Steinsiek.Odin.Modules.Core.Shared.DTOs;

/// <summary>
/// Standardized error response for all API errors.
/// </summary>
public sealed record class ErrorDetails
{
    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public required int StatusCode { get; init; }

    /// <summary>
    /// A human-readable error message.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The error type classification.
    /// </summary>
    public required OdinErrorType ErrorType { get; init; }

    /// <summary>
    /// The UTC timestamp when the error occurred.
    /// </summary>
    public required DateTime Timestamp { get; init; }
}
