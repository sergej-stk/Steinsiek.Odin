namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// BCrypt-based implementation of the password hasher.
/// </summary>
public sealed class BcryptPasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc />
    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
