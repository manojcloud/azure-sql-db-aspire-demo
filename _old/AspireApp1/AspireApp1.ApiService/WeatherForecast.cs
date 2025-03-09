using Microsoft.Data.SqlClient;

namespace AspireApp1.ApiService;

public record WeatherForecastData(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class WeatherForecast(SqlConnection conn)
{
    public IEnumerable<WeatherForecastData> GetWeatherForecast()
    {
        using (conn)
        {
            string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

            conn.Open();
            var cmd = new SqlCommand("SELECT TOP(10) [Id], [Date], [DegreesCelsius] FROM [dbo].[WeatherForecasts]", conn);
            var r = cmd.ExecuteReader();            
            while (r.Read())
            {
                var d = new WeatherForecastData
                (
                    DateOnly.FromDateTime(r.GetDateTime(1)),
                    r.GetInt32(2),
                    summaries[r.GetInt32(2) % summaries.Length]
                );

                yield return d;
            }            

            conn.Close();
        }
    }   
}

