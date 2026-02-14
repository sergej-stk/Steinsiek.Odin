namespace Steinsiek.Odin.Modules.Auth.Shared;

/// <summary>
/// Defines standard role names used for authorization throughout the Odin platform.
/// </summary>
public static class OdinRoles
{
    /// <summary>
    /// Administrator role with full system access including audit logs and user management.
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Manager role with CRUD access to persons and companies.
    /// </summary>
    public const string Manager = "Manager";

    /// <summary>
    /// Standard user role with limited CRUD access.
    /// </summary>
    public const string User = "User";

    /// <summary>
    /// Read-only role with view-only access to all data.
    /// </summary>
    public const string ReadOnly = "ReadOnly";

    /// <summary>
    /// Combined Admin and Manager roles for authorization attributes requiring either role.
    /// </summary>
    public const string AdminOrManager = "Admin,Manager";
}
