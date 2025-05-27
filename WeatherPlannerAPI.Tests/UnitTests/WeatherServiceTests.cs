using Xunit;
using Microsoft.Extensions.Configuration;
using WeatherPlannerAPI;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Moq;
using Moq.Protected;
using System.Threading;
using System.Collections.Generic;

namespace WeatherPlannerAPI.Tests.UnitTests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_InvalidCity_ReturnsNull()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"OpenWeatherMap:ApiKey", "fake_api_key"},
                    {"OpenWeatherMap:City", "INVALID_CITY"},
                    {"OpenWeatherMap:Units", "metric"}
                }).Build();

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(handlerMock.Object);
            var service = new WeatherService(client, config);

            var result = await service.GetWeatherAsync();

            Assert.Null(result);
        }
    }
}