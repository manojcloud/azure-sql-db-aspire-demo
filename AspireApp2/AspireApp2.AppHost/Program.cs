var builder = DistributedApplication.CreateBuilder(args);

builder.AddParameter("sql-password");

// SQL Server
var sqlSrv = builder.AddSqlServer("sql", port: 1433)
    .WithLifetime(ContainerLifetime.Persistent);

// Azure SQL
// var sqlSrv = builder.AddAzureSqlServer("sql")
//     .RunAsContainer();    

var sqlDb = sqlSrv.AddDatabase("aspiretodo");

var dbPrj = builder.AddSqlProject<Projects.TodoDB>("tododb")    
    .WithReference(sqlDb)
    .WaitFor(sqlDb);    

var dab = builder.AddDataAPIBuilder("dab")
    .WithReference(sqlDb)    
    .WaitFor(sqlDb)
    .WaitForCompletion(dbPrj);

builder.AddNpmApp("frontend", "../Frontend")
    .WithReference(dab)
    .WaitFor(dab)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
