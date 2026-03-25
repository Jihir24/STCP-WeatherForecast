namespace STCP_WeatherForecast.Observers
{
    // Observer interface
    public interface IWeatherObserver
    {
        void Update(double temperature);
    }
}