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

		public ChartData GetWeatherStats()
		{
			var locations = _weatherContext.Locations.ToList();

			var locationDataList = new List<ChartLocationData>();
			var minTemperatures = new List<float>();
			var maxWindspeed = new List<float>();

			if (locations.Any())
			{
				locations.ForEach(location =>
				{
					locationDataList.Add(new ChartLocationData()
					{
						Id = location.Id,
						City = location.City,
						Country = location.Country,
						LatestUpdate = _weatherContext.WeatherData.Where(x => x.LocationId == location.Id)
						.Max(lastestUpdate => lastestUpdate.Time)
					});
					minTemperatures.Add((float)_weatherContext.WeatherData.Where(x => x.LocationId == location.Id)
						.Min(weatherData => Convert.ToDouble(weatherData.Temperature)));
					maxWindspeed.Add((float)_weatherContext.WeatherData.Where(x => x.LocationId == location.Id)
						.Max(weatherData => Convert.ToDouble(weatherData.WindSpeed)));
				});
			}
			return CreateChartData(minTemperatures, maxWindspeed, locationDataList);
		}

		public GraphModel GetWindGraph(int locationId)
		{
			var list = new List<GraphData>();
			var location = _weatherContext.Locations.First(x => x.Id == locationId);
			var windData = _weatherContext.WeatherData.Where(x => x.LocationId == locationId && x.Time >= DateTime.UtcNow.AddHours(-2)).OrderBy(x => x.Time);
			var graphData = windData.Select(x => new GraphData() { Time = x.Time, Value = (float)x.WindSpeed });
			var latestUpdate = _weatherContext.WeatherData.Where(x => x.LocationId == locationId)
				.Max(lastestUpdate => lastestUpdate.Time);

			list.AddRange(graphData);

			return new GraphModel()
			{
				Country = location.Country,
				City = location.City,
				GraphData = list,
				LatestUpDateTime = latestUpdate,
				Label = $"{location.City} windspeed graph"
			};
		}
		public GraphModel GetTempGraph(int locationId)
		{
			var list = new List<GraphData>();

			var location = _weatherContext.Locations.First(x => x.Id == locationId);

			var windData = _weatherContext.WeatherData.Where(x => x.LocationId == locationId && x.Time >= DateTime.UtcNow.AddHours(-2)).OrderBy(x => x.Time);
			var graphData = windData.Select(x => new GraphData() { Time = x.Time, Value = (float)x.Temperature });
			var latestUpdate = _weatherContext.WeatherData.Where(x => x.LocationId == locationId)
				.Max(lastestUpdate => lastestUpdate.Time);

			list.AddRange(graphData);

			return new GraphModel()
			{
				Country = location.Country,
				City = location.City,
				GraphData = list,
				LatestUpDateTime = latestUpdate,
				Label = $"{location.City} temperature graph"
			};
		}

		private ChartData CreateChartData(List<float> temperatures, List<float> windspeeds, List<ChartLocationData> locations)
		{
			var chartData = new ChartData();
			var chartDatasets = new List<ChartDataset>();

			chartData.Locations = locations;
			chartDatasets.Add(new ChartDataset()
			{
				Label = "Min Temperature",
				Data = temperatures,
				BackgroundColor = "rgba(65, 105, 225, 0.5)",
				BorderColor = "rgba(0, 119, 190, 1)",
				BorderWidth = 1
			});
			chartDatasets.Add(new ChartDataset()
			{
				Label = "Max Windspeed",
				Data = windspeeds,
				BackgroundColor = "rgba(144, 238, 144, 0.5)",
				BorderColor = "rgba(124, 252, 0, 1)",
				BorderWidth = 1
			});

			chartData.Datasets = chartDatasets;
			return chartData;
		}
	}
}
