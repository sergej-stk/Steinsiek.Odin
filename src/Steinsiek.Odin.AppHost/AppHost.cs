Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Steinsiek.Odin AppHost...");

try
{
    var builder = DistributedApplication.CreateBuilder(args);

    builder.Services.AddSerilog((_, configuration) => configuration
        .ReadFrom.Configuration(builder.Configuration));

    var redis = builder.AddRedis("cache")
        .WithImageTag("7.4")
        .WithoutHttpsCertificate();

    var postgres = builder.AddPostgres("postgres")
        .WithPgAdmin();
    var odinDb = postgres.AddDatabase("odindb");

    var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
        .WithReference(redis)
        .WithReference(odinDb)
        .WaitFor(redis)
        .WaitFor(postgres)
        .WithEnvironment("DatabaseProvider", "PostgreSQL")
        .WithIconName("ShoppingBag", IconVariant.Filled);

    builder.AddProject<Projects.Steinsiek_Odin_Web>("web")
        .WithReference(api)
        .WaitFor(api)
        .WithExternalHttpEndpoints()
        .WithIconName("Globe", IconVariant.Filled);

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
