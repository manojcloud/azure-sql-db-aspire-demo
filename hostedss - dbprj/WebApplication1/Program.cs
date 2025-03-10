using Microsoft.Data.SqlClient;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.AddSqlServerClient(connectionName: "db");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (SqlConnection conn) =>
{
    var results = new List<WeatherForecast>();

    using (conn)
    {
        conn.Open();
        using (var cmd = new SqlCommand("SELECT TOP(10) [Id], [Date], [TemperatureC] FROM [dbo].[WeatherForecasts]", conn))
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var wf = new WeatherForecast(
                        DateOnly.FromDateTime(reader.GetDateTime(1)),
                        reader.GetInt32(2),
                        summaries[reader.GetInt32(0) % summaries.Length]
                    );

                    results.Add(wf);
                }
            }
        }
    }

    return results;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
