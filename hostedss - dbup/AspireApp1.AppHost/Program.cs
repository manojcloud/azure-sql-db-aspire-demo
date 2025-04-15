var builder = DistributedApplication.CreateBuilder(args);

// If you plan to use SQL Server
// var sqlSrv = builder.AddSqlServer("sqlsrv", port:1435).
//     WithLifetime(ContainerLifetime.Persistent);

// If you plan to deploy in Azure SQL Server
var sqlSrv = builder.AddAzureSqlServer("sqlsrv").RunAsContainer();

var db = sqlSrv.AddDatabase("db");

var dbDeploy = builder.AddProject<Projects.DatabaseDeploy>("dbDeploy")
    .WithReference(db)
    .WaitFor(db);

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
    .WaitFor(db)
    .WaitForCompletion(dbDeploy);

builder.Build().Run();
