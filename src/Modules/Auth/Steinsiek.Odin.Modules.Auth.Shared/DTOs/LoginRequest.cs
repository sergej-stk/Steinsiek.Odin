namespace Steinsiek.Odin.Modules.Auth.Shared.DTOs;

/// <summary>
/// Request payload for user login.
/// </summary>
public sealed record class LoginRequest
{
    /// <summary>
    /// The user's email address.
    /// </summary>
    [Required, EmailAddress]
    public required string Email { get; init; }

    /// <summary>
    /// The user's password (minimum 6 characters).
    /// </summary>
    [Required, MinLength(6)]
    public required string Password { get; init; }
}
