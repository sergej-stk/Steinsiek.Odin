Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Steinsiek.Odin AppHost...");

try
{
    var builder = DistributedApplication.CreateBuilder(args);

    builder.Services.AddSerilog((_, configuration) => configuration
        .ReadFrom.Configuration(builder.Configuration));

    var redis = builder.AddRedis("cache");

    var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
        .WithReference(redis)
        .WaitFor(redis)
        .WithIconName("ShoppingBag", IconVariant.Filled);

    builder.Build().Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
