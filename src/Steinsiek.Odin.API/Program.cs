Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Steinsiek.Odin API...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    // Service Defaults (Aspire)
    builder.AddServiceDefaults();

    // Redis
    builder.AddRedisClient("cache");

    // API Versioning
    builder.Services.AddApiVersioningConfiguration();

    // Controllers from all modules
    builder.Services.AddControllers()
        .AddApplicationPart(typeof(Steinsiek.Odin.Modules.Auth.AuthModule).Assembly)
        .AddApplicationPart(typeof(Steinsiek.Odin.Modules.Products.ProductsModule).Assembly);

    // OpenAPI with API version support
    builder.Services.AddOpenApi("v1", options =>
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            document.Info.Title = "Steinsiek.Odin API";
            document.Info.Version = "v1";
            document.Info.Description = "E-Commerce REST API - Version 1";

            document.Components ??= new OpenApiComponents();
            if (document.Components.SecuritySchemes is not null)
            {
                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Bearer token. Obtain via POST /api/v1/auth/login"
                };
            }

            return Task.CompletedTask;
        });

        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var metadata = context.Description.ActionDescriptor.EndpointMetadata;
            var hasAuthorize = metadata.OfType<AuthorizeAttribute>().Any();
            var hasAllowAnonymous = metadata.OfType<AllowAnonymousAttribute>().Any();

            if (hasAuthorize && !hasAllowAnonymous)
            {
                operation.Security ??= [];
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer")] = new List<string>()
                });
            }

            return Task.CompletedTask;
        });
    });

    // JWT Authentication
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddAuthorization();

    // Module Services
    builder.Services.AddModules();

    var app = builder.Build();

    // Log Context Enrichment
    app.UseLogContext();

    // Exception Handling
    app.UseExceptionHandling();

    // Serilog Request Logging
    app.UseSerilogRequestLogging();

    // Static Assets
    app.MapStaticAssets();

    // OpenAPI & Scalar (Development)
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi("/openapi/{documentName}/openapi.json");
        app.MapScalarApiReference(options =>
        {
            options.Title = "Steinsiek.Odin API";
            options.Theme = ScalarTheme.Purple;
            options.Favicon = "/images/logo.svg";
            options.AddDocument("v1", "API v1", "/openapi/v1/openapi.json");
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Aspire Default Endpoints
    app.MapDefaultEndpoints();

    Log.Information("Steinsiek.Odin API started successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
