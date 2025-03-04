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
            var cmd = new SqlCommand("SELECT [value] AS id FROM GENERATE_SERIES(1,5)", conn);
            var r = cmd.ExecuteReader();            
            while (r.Read())
            {
                var d = new WeatherForecastData
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(r.GetInt32(0))),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                );

                yield return d;
            }            

            conn.Close();
        }
    }   
}

