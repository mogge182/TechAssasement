using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechAssasementMVC.Enums;
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


		public IActionResult GetData(int locationId, GraphType type)
		{

			switch (type)
			{
				case GraphType.Temperatur:
					return Ok(_weatherRepository.GetTempGraph(locationId));
				case GraphType.Wind:
					return Ok(_weatherRepository.GetWindGraph(locationId));
				default:
					return BadRequest();
			}
		}
	}
}