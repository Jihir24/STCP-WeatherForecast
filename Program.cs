using Microsoft.EntityFrameworkCore;
using STCP_WeatherForecast.Data;
using STCP_WeatherForecast.Services;
using STCP_WeatherForecast.Observers;

var builder = WebApplication.CreateBuilder(args);

// Register database (ORM)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=weather.db"));

// Register service (Dependency Injection)
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

// Create database automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Endpoint: get new weather
app.MapGet("/weather", async (WeatherService service) =>
{
    service.AddObserver(new ConsoleWeatherObserver());

    var temp = await service.GetWeatherFromApiAsync();
    await service.SaveWeatherAsync(temp);

    return Results.Ok(new { temperature = temp });
});

// Endpoint: get full history
app.MapGet("/history", (WeatherService service) =>
{
    return Results.Ok(service.GetHistory());
});

// Endpoint: get last 5 measurements
app.MapGet("/history/last5", (WeatherService service) =>
{
    return Results.Ok(service.GetLastFive());
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();