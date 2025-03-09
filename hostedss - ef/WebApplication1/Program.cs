using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

// Add DB contenxt from Aspire
builder.AddSqlServerDbContext<WeatherForecastContext>(connectionName: "db");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (WeatherForecastContext context) =>
{
    return context.WeatherForecasts.Take(10).ToList();
});

app.Run();

public class WeatherForecastData
{
    private string[] summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary => summaries[this.Id % summaries.Length];
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class WeatherForecastContext(DbContextOptions options) : DbContext(options) {
    public DbSet<WeatherForecastData> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecastData>()
            .Property(e => e.Id)
            .UseIdentityColumn();
    }
}