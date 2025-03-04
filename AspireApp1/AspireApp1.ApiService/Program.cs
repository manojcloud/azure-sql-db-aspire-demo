using AspireApp1.ApiService;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Aspire client integrations
builder.AddSqlServerClient(connectionName: "aspiredb");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/weatherforecast", (SqlConnection connection) =>
{
    var wf = new WeatherForecast(connection);
    return wf.GetWeatherForecast();
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

