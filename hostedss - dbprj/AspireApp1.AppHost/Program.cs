var builder = DistributedApplication.CreateBuilder(args);

var sqlSrv = builder.AddSqlServer("sqlsrv").
    WithLifetime(ContainerLifetime.Persistent);

var db = sqlSrv.AddDatabase("db");

var dbPrj = builder.AddSqlProject<Projects.WeatherDatabase>("dbPrj")
    .WithConfigureDacDeployOptions((options) => options.AllowTableRecreation = false)
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.WebApplication1>("webapi")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(dbPrj);

builder.Build().Run();
