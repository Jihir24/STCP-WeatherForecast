using System.Text.Json;
using STCP_WeatherForecast.Data;
using STCP_WeatherForecast.Models;

namespace STCP_WeatherForecast.Services
{
    // Handles API calls and database logic
    public class WeatherService
    {
        private readonly AppDbContext _context;

        public WeatherService(AppDbContext context)
        {
            _context = context;
        }

        // Fetch weather from API
        public async Task<double> GetWeatherFromApiAsync()
        {
            using var client = new HttpClient();

            var url = "https://api.open-meteo.com/v1/forecast?latitude=55.6761&longitude=12.5683&current_weather=true";
            var response = await client.GetStringAsync(url);

            var json = JsonDocument.Parse(response);

            return json.RootElement
                .GetProperty("current_weather")
                .GetProperty("temperature")
                .GetDouble();
        }

        // Save data to database
        public async Task SaveWeatherAsync(double temperature)
        {
            var weather = new Weather
            {
                Temperature = temperature,
                Time = DateTime.Now.ToString("HH:mm:ss")
            };

            _context.WeatherData.Add(weather);
            await _context.SaveChangesAsync();
        }

        // Get history
        public List<Weather> GetHistory()
        {
            return _context.WeatherData
                .OrderByDescending(w => w.Id)
                .ToList();
        }
    }
}