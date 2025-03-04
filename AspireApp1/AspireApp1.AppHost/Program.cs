var builder = DistributedApplication.CreateBuilder(args);

builder.AddParameter("sql-password");

var sql = builder.AddSqlServer("sql", port:1433)
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("tempdb");

var apiService = builder.AddProject<Projects.AspireApp1_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.AspireApp1_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
