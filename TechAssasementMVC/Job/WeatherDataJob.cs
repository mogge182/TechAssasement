using Quartz;
using System.Text.Json;
using TechAssasementMVC.Database;
using TechAssasementMVC.Dtos;

namespace TechAssasementMVC.Job
{
	public class WeatherDataJob : IJob
    {
        private readonly WeatherContext _weatherContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public WeatherDataJob(
            IConfiguration configuration,
            WeatherContext context,
            HttpClient httpClient)
        {
            _configuration = configuration;
            _weatherContext = context;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("Api:BaseAddress"));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
              ;
                var locations = _weatherContext.Locations.ToList();

                foreach (var location in locations)
                {
                    var result = _httpClient.GetAsync(string.Format(
                        _configuration.GetValue<string>("Api:WeatherEndpoint"),
                        _configuration.GetValue<string>("Api:Key"),
                        location.City));

                    var weatherData = await result.Result.Content.ReadAsStringAsync();

                    var weatherDataDto = JsonSerializer.Deserialize<WeatherDataDto>(weatherData);

                    _weatherContext.WeatherData.Add(new WeatherData()
                    {
                        LocationId = location.Id,
                        Clouds = weatherDataDto.Current.Cloud,
                        Temperature = weatherDataDto.Current.Temp_c,
                        WindSpeed = weatherDataDto.Current.Wind_kph,
                        Time = DateTimeOffset.FromUnixTimeSeconds(weatherDataDto.Location.Localtime_epoch).UtcDateTime
                    });

                    await _weatherContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

