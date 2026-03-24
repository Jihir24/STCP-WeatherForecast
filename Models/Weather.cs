namespace STCP_WeatherForecast.Models
{
    // Represents one weather snapshot
    public class Weather
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public string Time { get; set; } = "";
    }
}