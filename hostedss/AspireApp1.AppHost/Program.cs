var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sqlsrv-password");

var sqlSrv = builder.AddSqlServer("sqlsrv", sqlPassword, port:1435)
    .WithLifetime(ContainerLifetime.Persistent);

//var db = sqlSrv.AddDatabase("db");
var db = sqlSrv.AddDatabase("db", databaseName: "tempdb");

builder.AddProject<Projects.WebApplication1>("webapi")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Forecast API (HTTP)";
        url.Url += "/weatherforecast";
    })
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Forecast API (HTTPS)";
        url.Url += "/weatherforecast";
    })
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
