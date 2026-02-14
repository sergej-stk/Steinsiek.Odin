namespace Steinsiek.Odin.Modules.Auth.Entities;

/// <summary>
/// Represents an authenticated user in the system.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the hashed password.
    /// </summary>
    public required string PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets the user's full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Gets or sets whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the user's preferred language code (e.g. "de", "en").
    /// </summary>
    public string PreferredLanguage { get; set; } = "en";

    /// <summary>
    /// Gets or sets the user-role assignments for this user.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
