namespace Steinsiek.Odin.Modules.Auth.Shared;

/// <summary>
/// Defines JWT registered claim names for consistent claim access across the platform.
/// </summary>
public static class JwtClaimNames
{
    /// <summary>
    /// Subject claim containing the user identifier. Maps to JwtRegisteredClaimNames.Sub.
    /// </summary>
    public const string Subject = "sub";

    /// <summary>
    /// Email address claim. Maps to JwtRegisteredClaimNames.Email.
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// Given name (first name) claim. Maps to JwtRegisteredClaimNames.GivenName.
    /// </summary>
    public const string GivenName = "given_name";

    /// <summary>
    /// Family name (last name) claim. Maps to JwtRegisteredClaimNames.FamilyName.
    /// </summary>
    public const string FamilyName = "family_name";
}
