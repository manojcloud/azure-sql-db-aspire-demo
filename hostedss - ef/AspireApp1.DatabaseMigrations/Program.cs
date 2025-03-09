using AspireApp1.DatabaseMigrations;
using WebApplication1;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("DatabaseMigrations"));

builder.AddSqlServerDbContext<WeatherForecastContext>(connectionName: "db");

var host = builder.Build();

host.Run();
