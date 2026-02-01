namespace Steinsiek.Odin.Modules.Auth.Services;

/// <summary>
/// Implementation of the authentication service handling login and registration.
/// </summary>
public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The logger instance.</param>
    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
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

        var passwordHash = InMemoryUserRepository.ComputeHash(request.Password);
        if (user.PasswordHash != passwordHash)
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
        return GenerateTokenResponse(user);
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
            PasswordHash = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _userRepository.Add(user, cancellationToken);
        _logger.LogInformation("Registration successful for {Email}", request.Email);

        return GenerateTokenResponse(user);
    }

    private LoginResponse GenerateTokenResponse(User user)
    {
        var expiresAt = DateTime.UtcNow.AddHours(
            _configuration.GetValue("Jwt:ExpirationHours", 24));

        var token = GenerateJwtToken(user, expiresAt);

        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }
        };
    }

    private string GenerateJwtToken(User user, DateTime expiresAt)
    {
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured");
        var issuer = _configuration["Jwt:Issuer"] ?? "Steinsiek.Odin";
        var audience = _configuration["Jwt:Audience"] ?? "Steinsiek.Odin.API";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

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
