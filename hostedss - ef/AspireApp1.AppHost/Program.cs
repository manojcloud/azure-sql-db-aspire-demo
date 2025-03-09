var builder = DistributedApplication.CreateBuilder(args);

var sqlSrv = builder.AddSqlServer("sqlsrv").
    WithLifetime(ContainerLifetime.Persistent);

var db = sqlSrv.AddDatabase("db", "aspiredb");

var dbMigrate = builder.AddProject<Projects.AspireApp1_DatabaseMigrations>("dbMigrate")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.WebApplication1>("webapi")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(dbMigrate);

builder.Build().Run();
