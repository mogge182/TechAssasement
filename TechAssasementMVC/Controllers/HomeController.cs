using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechAssasementMVC.Models;
using TechAssasementMVC.Repositories;

namespace TechAssasementMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly WeatherRepository _weatherRepository;
		public HomeController(ILogger<HomeController> logger, WeatherRepository weatherRepository)
		{
			_logger = logger;
			_weatherRepository = weatherRepository;
		}

		public IActionResult Index()
		{
			var stats = _weatherRepository.GetWeatherStats();
			if (stats == null)
			{
				return View();
			}
			return View(stats);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public IActionResult GetData(int locationId, int type)
		{
			var graphData = _weatherRepository.GetWindGraph(locationId);
			return Ok(graphData);
		}
	}
}