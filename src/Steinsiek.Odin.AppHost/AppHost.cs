var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
    .WithReference(redis)
    .WaitFor(redis);

builder.Build().Run();
