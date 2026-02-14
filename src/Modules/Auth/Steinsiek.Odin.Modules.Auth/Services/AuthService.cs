namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// Implementation of the authentication service handling login and registration.
/// </summary>
public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="passwordHasher">The password hasher.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The logger instance.</param>
    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<LoginResponse?> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for {Email}", request.Email);

        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);
        if (user is null)
        {
            _logger.LogWarning("Login failed: User {Email} not found", request.Email);
            return null;
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Login failed: Invalid password for {Email}", request.Email);
            return null;
        }

        if (!user.IsActive)
        {
            _logger.LogWarning("Login failed: User {Email} is inactive", request.Email);
            return null;
        }

        _logger.LogInformation("Login successful for {Email}", request.Email);
        return await GenerateTokenResponse(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<LoginResponse?> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registration attempt for {Email}", request.Email);

        var existingUser = await _userRepository.GetByEmail(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            _logger.LogWarning("Registration failed: Email {Email} already exists", request.Email);
            return null;
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _userRepository.Add(user, cancellationToken);
        _logger.LogInformation("Registration successful for {Email}", request.Email);

        return await GenerateTokenResponse(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<RoleDto>> GetAllRoles(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return
        [
            new RoleDto { Id = Persistence.Configurations.RoleConfiguration.AdminRoleId, Name = OdinRoles.Admin, Description = "Full access including audit log, user management and roles" },
            new RoleDto { Id = Persistence.Configurations.RoleConfiguration.ManagerRoleId, Name = OdinRoles.Manager, Description = "CRUD access to persons and companies" },
            new RoleDto { Id = Persistence.Configurations.RoleConfiguration.UserRoleId, Name = OdinRoles.User, Description = "Limited CRUD access to persons and companies" },
            new RoleDto { Id = Persistence.Configurations.RoleConfiguration.ReadOnlyRoleId, Name = OdinRoles.ReadOnly, Description = "Read-only access to all data" }
        ];
    }

    /// <inheritdoc />
    public async Task<bool> AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        return await _userRepository.AssignRole(userId, roleId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> RemoveRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        return await _userRepository.RemoveRole(userId, roleId, cancellationToken);
    }

    private async Task<LoginResponse> GenerateTokenResponse(User user, CancellationToken cancellationToken)
    {
        var expiresAt = DateTime.UtcNow.AddHours(
            _configuration.GetValue(ConfigKeys.Jwt.ExpirationHours, ConfigKeys.Jwt.DefaultExpirationHours));

        var roles = await _userRepository.GetRoles(user.Id, cancellationToken);
        var token = GenerateJwtToken(user, roles, expiresAt);

        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            }
        };
    }

    private string GenerateJwtToken(User user, IReadOnlyList<string> roles, DateTime expiresAt)
    {
        var key = _configuration[ConfigKeys.Jwt.Key]
            ?? throw new InvalidOperationException("JWT Key is not configured");
        var issuer = _configuration[ConfigKeys.Jwt.Issuer] ?? ConfigKeys.Jwt.DefaultIssuer;
        var audience = _configuration[ConfigKeys.Jwt.Audience] ?? ConfigKeys.Jwt.DefaultAudience;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
