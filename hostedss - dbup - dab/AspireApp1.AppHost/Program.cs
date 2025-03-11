var builder = DistributedApplication.CreateBuilder(args);

var sqlSrv = builder.AddSqlServer("sqlsrv", port:1435)
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlSrv.AddDatabase("db");

var dbDeploy = builder.AddProject<Projects.DatabaseDeploy>("dbDeploy")
    .WithReference(db)
    .WaitFor(db);

builder.AddDataAPIBuilder("dab")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(dbDeploy);

builder.Build().Run();
