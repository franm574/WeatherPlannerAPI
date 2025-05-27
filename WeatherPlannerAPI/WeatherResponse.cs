using System.Text.Json.Serialization;

namespace WeatherPlannerAPI
{
    public class WeatherResponse
    {
        [JsonPropertyName("main")]
        public WeatherMain Main { get; set; } = new WeatherMain();

        [JsonPropertyName("weather")]
        public List<WeatherDetail> Weather { get; set; } = new List<WeatherDetail>();
    }

    public class WeatherMain
    {
        [JsonPropertyName("temp")]
        public float Temp { get; set; }
    }

    public class WeatherDetail
    {
        [JsonPropertyName("main")]
        public string Main { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}