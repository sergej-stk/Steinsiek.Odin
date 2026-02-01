namespace Steinsiek.Odin.Modules.Auth.Shared.DTOs;

/// <summary>
/// Request payload for user registration.
/// </summary>
public sealed record class RegisterRequest
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

    /// <summary>
    /// The user's first name.
    /// </summary>
    [Required]
    public required string FirstName { get; init; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    [Required]
    public required string LastName { get; init; }
}
