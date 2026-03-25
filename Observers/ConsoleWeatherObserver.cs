namespace STCP_WeatherForecast.Observers
{
    // Logs new temperature updates
    public class ConsoleWeatherObserver : IWeatherObserver
    {
        public void Update(double temperature)
        {
            Console.WriteLine($"New temperature: {temperature} °C");
        }
    }
}