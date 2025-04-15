using Azure.Provisioning.Sql;

var builder = DistributedApplication.CreateBuilder(args);

// If you plan to use SQL Server
// var sqlSrv = builder.AddSqlServer("sqlsrv", port:1435).
//     WithLifetime(ContainerLifetime.Persistent);

// If you plan to deploy in Azure SQL Server
var sqlSrv = builder.AddAzureSqlServer("sqlsrv")
    .ConfigureInfrastructure(infra => {
        var azureResources = infra.GetProvisionableResources();
        var azureDb = azureResources.OfType<SqlDatabase>().Single();
        azureDb.Sku = new SqlSku() { Name = "GP_Gen5_2" };
    })
    .RunAsContainer();

var db = sqlSrv.AddDatabase("db", "aspiredb");

var dbMigrate = builder.AddProject<Projects.AspireApp1_DatabaseMigrations>("dbmigrate")
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
    .WaitForCompletion(dbMigrate);

builder.Build().Run();
