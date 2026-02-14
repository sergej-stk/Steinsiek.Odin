Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Steinsiek.Odin Web...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Logging.ClearProviders();
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services), writeToProviders: true);

    // Service Defaults (Aspire)
    builder.AddServiceDefaults();

    // Blazor
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    // Localization
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    // Toast Notifications
    builder.Services.AddScoped<IToastService, ToastService>();

    // Authentication State
    builder.Services.AddScoped<JwtAuthenticationStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
        sp.GetRequiredService<JwtAuthenticationStateProvider>());
    builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();
    builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();

    // API Clients via Aspire service discovery
    // Note: Auth header is set by each client directly (not via DelegatingHandler)
    // because IHttpClientFactory handler scopes cannot access the circuit's ProtectedSessionStorage.
    builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    });

    builder.Services.AddHttpClient<IPersonApiClient, PersonApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    });

    builder.Services.AddHttpClient<ICompanyApiClient, CompanyApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    });

    builder.Services.AddHttpClient<IDashboardApiClient, DashboardApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    });

    builder.Services.AddHttpClient<ILookupApiClient, LookupApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    });


    var app = builder.Build();

    // Serilog Request Logging
    app.UseSerilogRequestLogging();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    // Request Localization
    var supportedCultures = new[] { "en", "de" };
    app.UseRequestLocalization(options =>
    {
        options.SetDefaultCulture("en");
        options.AddSupportedCultures(supportedCultures);
        options.AddSupportedUICultures(supportedCultures);
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapStaticAssets();
    app.UseAntiforgery();

    app.MapRazorComponents<Steinsiek.Odin.Web.Components.App>()
        .AddInteractiveServerRenderMode();

    // Aspire Default Endpoints
    app.MapDefaultEndpoints();

    Log.Information("Steinsiek.Odin Web started successfully");
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
