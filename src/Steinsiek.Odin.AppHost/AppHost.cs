var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
    .WithReference(redis)
    .WaitFor(redis)
    .WithIconName("ShoppingBag", IconVariant.Filled);

builder.Build().Run();
