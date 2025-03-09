using Microsoft.Data.SqlClient;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__db");

    var results = new List<WeatherForecast>();

    using (var conn = new SqlConnection(connectionString))
    {
        conn.Open();
        using (var cmd = new SqlCommand("select [value] from generate_series(1,10)", conn))
        {
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var wf = new WeatherForecast(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(reader.GetInt32(0))),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
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
