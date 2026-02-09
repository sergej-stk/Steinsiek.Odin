namespace Steinsiek.Odin.API.Tests;

/// <summary>
/// Custom web application factory that provides test-safe configuration for Aspire-managed services.
/// </summary>
public sealed class OdinWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:cache", "localhost:0,abortConnect=false,connectTimeout=1");
    }
}
