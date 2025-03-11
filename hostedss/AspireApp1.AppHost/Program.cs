var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sqlsrv-password");

var sqlSrv = builder.AddSqlServer("sqlsrv", sqlPassword, port:1435).
    WithLifetime(ContainerLifetime.Persistent);

//var db = sqlSrv.AddDatabase("db");
var db = sqlSrv.AddDatabase("db", databaseName: "tempdb");

builder.AddProject<Projects.WebApplication1>("webapi")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
