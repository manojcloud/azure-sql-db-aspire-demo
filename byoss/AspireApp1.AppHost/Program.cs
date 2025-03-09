var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddConnectionString("db");

builder.AddProject<Projects.WebApplication1>("webapi")
    .WithReference(db);

builder.Build().Run();
