namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// Provides password hashing and verification capabilities.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified plaintext password.
    /// </summary>
    /// <param name="password">The plaintext password to hash.</param>
    /// <returns>The hashed password.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies whether a plaintext password matches the specified hash.
    /// </summary>
    /// <param name="password">The plaintext password to verify.</param>
    /// <param name="hash">The hash to verify against.</param>
    /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
    bool Verify(string password, string hash);
}
