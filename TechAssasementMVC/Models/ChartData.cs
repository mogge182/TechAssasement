namespace TechAssasementMVC.Models
{
	public class ChartData
	{
		public string[] Labels { get; set; }
		public List<ChartDataset> Datasets { get; set; }
	}

	public class ChartDataset
	{
		public string Label { get; set; }
		public string BackgroundColor { get; set; }
		public string BorderColor { get; set; }
		public int BorderWidth { get; set; } = 1;
		public bool Fill { get; set; }
		public float[] Data { get; set; }
	}
}
