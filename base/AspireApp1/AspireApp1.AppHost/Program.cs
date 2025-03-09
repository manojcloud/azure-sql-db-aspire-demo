var builder = DistributedApplication.CreateBuilder(args);

//var sql = builder.AddSqlServer("sql", port: 1433);
//var db = sql.AddDatabase("db");

var db = builder.AddConnectionString("db");

var dbDeploy = builder.AddProject<Projects.DatabaseDeploy>("db-deploy")
    .WithReference(db)
    .WaitFor(db);

var web = builder.AddProject<Projects.WebApplication1>("web")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(dbDeploy);

builder.Build().Run();
