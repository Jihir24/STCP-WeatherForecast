using System.Text.Json;
using STCP_WeatherForecast.Data;
using STCP_WeatherForecast.Models;
using STCP_WeatherForecast.Observers;

namespace STCP_WeatherForecast.Services
{
    // Handles API calls and database logic
    public class WeatherService
    {
        private readonly AppDbContext _context;
        private readonly List<IWeatherObserver> _observers = new();

        public WeatherService(AppDbContext context)
        {
            _context = context;
        }

        // Adds an observer
        public void AddObserver(IWeatherObserver observer)
        {
            _observers.Add(observer);
        }

        // Notifies all observers when new temperature is received
        private void NotifyObservers(double temperature)
        {
            foreach (var observer in _observers)
            {
                observer.Update(temperature);
            }
        }

        // Fetch weather from API
        public async Task<double> GetWeatherFromApiAsync()
        {
            using var client = new HttpClient();

            var url = "https://api.open-meteo.com/v1/forecast?latitude=55.6761&longitude=12.5683&current_weather=true";
            var response = await client.GetStringAsync(url);

            var json = JsonDocument.Parse(response);

            var temperature = json.RootElement
                .GetProperty("current_weather")
                .GetProperty("temperature")
                .GetDouble();

            NotifyObservers(temperature);

            return temperature;
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

        // Get full history
        public List<Weather> GetHistory()
        {
            return _context.WeatherData
                .OrderByDescending(w => w.Id)
                .ToList();
        }

        // Get the last 5 weather measurements
        public List<Weather> GetLastFive()
        {
            return _context.WeatherData
                .OrderByDescending(w => w.Id)
                .Take(5)
                .ToList();
        }
    }
}