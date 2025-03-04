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
            var cmd = new SqlCommand("SELECT TOP(10) [Date], [DegreesCelsius] AS id FROM [dbo].[WeatherForecasts]", conn);
            var r = cmd.ExecuteReader();            
            while (r.Read())
            {
                var d = new WeatherForecastData
                (
                    DateOnly.FromDateTime(r.GetDateTime(0)),
                    r.GetInt32(1),
                    summaries[0]
                );

                yield return d;
            }            

            conn.Close();
        }
    }   
}

