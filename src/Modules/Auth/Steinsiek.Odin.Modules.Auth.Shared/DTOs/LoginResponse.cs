namespace Steinsiek.Odin.Modules.Auth.Shared.DTOs;

/// <summary>
/// Response payload after successful authentication.
/// </summary>
public sealed record class LoginResponse
{
    /// <summary>
    /// The JWT access token.
    /// </summary>
    public required string Token { get; init; }

    /// <summary>
    /// The token expiration timestamp.
    /// </summary>
    public required DateTime ExpiresAt { get; init; }

    /// <summary>
    /// The authenticated user's information.
    /// </summary>
    public required UserDto User { get; init; }
}

/// <summary>
/// Data transfer object for user information.
/// </summary>
public sealed record class UserDto
{
    /// <summary>
    /// The user's unique identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public required string LastName { get; init; }
}
