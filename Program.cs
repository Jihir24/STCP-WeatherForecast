using Microsoft.EntityFrameworkCore;
using STCP_WeatherForecast.Data;
using STCP_WeatherForecast.Services;

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
    var temp = await service.GetWeatherFromApiAsync();
    await service.SaveWeatherAsync(temp);

    return Results.Ok(new { temperature = temp });
});

// Endpoint: get history
app.MapGet("/history", (WeatherService service) =>
{
    return Results.Ok(service.GetHistory());
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();