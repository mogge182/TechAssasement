namespace TechAssasementMVC.Models
{
	public class LocationWeatherStat
	{
		public int LocationId { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public float MinTemperature { get; set; }
		public float HighestWindSpeed { get; set; }
		public DateTime LastUpdateTime { get; set; }

	}
}
