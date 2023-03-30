namespace TechAssasementMVC.Models
{

	public class GraphModel
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string Label { get; set; }
		public IEnumerable<GraphData> GraphData { get; set; }
	}

	public class GraphData
	{
		public double Value { get; set; }
		public DateTime Time { get; set; }
	}
}
