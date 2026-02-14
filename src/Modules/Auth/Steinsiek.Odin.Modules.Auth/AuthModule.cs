namespace Steinsiek.Odin.Modules.Auth;

/// <summary>
/// Module for authentication functionality including user management and JWT token generation.
/// </summary>
public sealed class AuthModule : IModule
{
    /// <inheritdoc />
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
    }
}
