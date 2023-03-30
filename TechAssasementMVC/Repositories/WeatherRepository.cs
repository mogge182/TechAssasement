using TechAssasementMVC.Database;
using TechAssasementMVC.Models;

namespace TechAssasementMVC.Repositories
{
	public class WeatherRepository
	{
		private readonly WeatherContext _weatherContext;
		public WeatherRepository(WeatherContext weatherContext)
		{
			_weatherContext = weatherContext;
		}

		public IEnumerable<LocationWeatherStat> GetWeatherStats()
		{
			var locations = _weatherContext.Locations.ToList();

			if (locations.Any())
			{
				var locationWeatherStats = new List<LocationWeatherStat>();
				locations.ForEach(location =>
				{
					locationWeatherStats.Add(new LocationWeatherStat
					{
						LocationId = location.Id,
						Country = location.Country,
						City = location.City,
						MinTemperature = (float)_weatherContext.WeatherData.Where(x => x.LocationId == location.Id).Min(weatherData => Convert.ToDouble(weatherData.Temperature)),
						HighestWindSpeed = (float)_weatherContext.WeatherData.Where(x => x.LocationId == location.Id).Max(weatherData => Convert.ToDouble(weatherData.WindSpeed)),
						LastUpdateTime = _weatherContext.WeatherData.Where(x => x.LocationId == location.Id).Max(lastestUpdate => lastestUpdate.Time)
					});
				});
				return locationWeatherStats;
			}
			return null;

		}

		public GraphModel GetWindGraph(int locationId)
		{
			var list = new List<GraphData>();
			var location = _weatherContext.Locations.First(x => x.Id == locationId);


			var windData = _weatherContext.WeatherData.Where(x => x.LocationId == locationId && x.Time >= DateTime.UtcNow.AddHours(-2)).OrderBy(x => x.Time);
			var graphData = windData.Select(x => new GraphData() { Time = x.Time, Value = (float)x.WindSpeed });

			list.AddRange(graphData);

			return new GraphModel()
			{
				Country = location.Country,
				City = location.City,
				GraphData = list,
				Label = $"{location.City} windspeed graph"
			};
		}
		public GraphModel GetTempGraph(int locationId)
		{
			var list = new List<GraphData>();

			var location = _weatherContext.Locations.First(x => x.Id == locationId);

			var windData = _weatherContext.WeatherData.Where(x => x.LocationId == locationId && x.Time >= DateTime.UtcNow.AddHours(-2)).OrderBy(x => x.Time);
			var graphData = windData.Select(x => new GraphData() { Time = x.Time, Value = (float)x.Temperature });

			list.AddRange(graphData);

			return new GraphModel()
			{
				Country = location.Country,
				City = location.City,
				GraphData = list,
				Label = $"{location.City} temperature graph"
			};
		}



	}
}
