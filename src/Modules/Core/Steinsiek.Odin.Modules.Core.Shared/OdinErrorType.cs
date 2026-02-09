namespace Steinsiek.Odin.Modules.Core.Shared;

/// <summary>
/// Classification of API error types for structured error handling.
/// </summary>
public enum OdinErrorType
{
    /// <summary>
    /// General unclassified error.
    /// </summary>
    General,

    /// <summary>
    /// Validation error due to invalid input.
    /// </summary>
    Validation,

    /// <summary>
    /// Requested element was not found.
    /// </summary>
    NotFound,

    /// <summary>
    /// Unauthorized access attempt.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// Business rule violation.
    /// </summary>
    BusinessRule,

    /// <summary>
    /// Internal server error.
    /// </summary>
    Internal
}
