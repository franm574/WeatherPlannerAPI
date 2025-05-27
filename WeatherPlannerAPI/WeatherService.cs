using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace WeatherPlannerAPI
{
    // This service connects to OpenWeatherMap and gets current weather data
    public class WeatherService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<WeatherResponse?> GetWeatherAsync() // Build the API URL using app settings
        {
            string apiKey = _config["OpenWeatherMap:ApiKey"]!;
            string city = _config["OpenWeatherMap:City"]!;
            string units = _config["OpenWeatherMap:Units"]!;

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units={units}";

            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<WeatherResponse>(stream); // Convert the JSON response into a WeatherResponse object
        }
    }
}