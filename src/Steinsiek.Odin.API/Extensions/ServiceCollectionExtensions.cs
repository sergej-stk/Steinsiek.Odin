namespace Steinsiek.Odin.API.Extensions;

/// <summary>
/// Extension methods for configuring application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all module services with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        AuthModule.RegisterServices(services);
        PersonsModule.RegisterServices(services);
        CompaniesModule.RegisterServices(services);
        return services;
    }

    /// <summary>
    /// Registers core infrastructure services including audit logging.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<Steinsiek.Odin.Modules.Core.Persistence.AuditSaveChangesInterceptor>();
        services.AddScoped<Steinsiek.Odin.Modules.Core.Repositories.IAuditLogRepository, Steinsiek.Odin.Modules.Core.Repositories.EfAuditLogRepository>();
        services.AddScoped<Steinsiek.Odin.Modules.Core.Services.IAuditLogService, Steinsiek.Odin.Modules.Core.Services.AuditLogService>();
        services.AddScoped<Steinsiek.Odin.Modules.Core.Services.ITranslationService, Steinsiek.Odin.Modules.Core.Services.TranslationService>();
        return services;
    }

    /// <summary>
    /// Registers the <see cref="OdinDbContext"/> with the appropriate database provider based on configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OdinDbContextOptions>(options =>
        {
            options.ModuleAssemblies.Add(typeof(Steinsiek.Odin.Modules.Core.Entities.BaseEntity).Assembly);
            options.ModuleAssemblies.Add(typeof(AuthModule).Assembly);
            options.ModuleAssemblies.Add(typeof(PersonsModule).Assembly);
            options.ModuleAssemblies.Add(typeof(CompaniesModule).Assembly);
        });

        var provider = configuration.GetValue<string>("DatabaseProvider") ?? "InMemory";

        services.AddDbContext<OdinDbContext>((_, options) =>
        {
            if (provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
            {
                options.UseNpgsql(configuration.GetConnectionString("odindb"));
            }
            else
            {
                options.UseInMemoryDatabase("OdinDb");
            }
        });

        return services;
    }

    /// <summary>
    /// Configures URL-based API versioning for the application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = false;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// Configures JWT Bearer authentication.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown when JWT Key is not configured.</exception>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured");
        var jwtIssuer = configuration["Jwt:Issuer"] ?? "Steinsiek.Odin";
        var jwtAudience = configuration["Jwt:Audience"] ?? "Steinsiek.Odin.API";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}
