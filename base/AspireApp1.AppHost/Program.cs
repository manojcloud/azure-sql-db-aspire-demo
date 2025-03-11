var builder = DistributedApplication.CreateBuilder(args);

var web = builder.AddProject<Projects.WebApplication1>("web");

builder.Build().Run();
