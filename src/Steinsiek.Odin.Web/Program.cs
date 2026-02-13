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

    // Toast Notifications
    builder.Services.AddScoped<IToastService, ToastService>();

    // Authentication State
    builder.Services.AddScoped<JwtAuthenticationStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
        sp.GetRequiredService<JwtAuthenticationStateProvider>());
    builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddAuthorizationCore();

    // API Clients via Aspire service discovery
    builder.Services.AddTransient<ApiAuthenticationHandler>();

    builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    }).AddHttpMessageHandler<ApiAuthenticationHandler>();

    builder.Services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    }).AddHttpMessageHandler<ApiAuthenticationHandler>();

    builder.Services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(client =>
    {
        client.BaseAddress = new Uri("https+http://api");
    }).AddHttpMessageHandler<ApiAuthenticationHandler>();

    var app = builder.Build();

    // Serilog Request Logging
    app.UseSerilogRequestLogging();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
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
