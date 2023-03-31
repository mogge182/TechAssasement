namespace TechAssasementMVC.Models
{
    public class ChartData
    {
        public List<ChartLocationData> Locations { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }
    public class ChartDataset
    {
        public string Label { get; set; }
        public List<float> Data { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int BorderWidth { get; set; }
    }

    public class ChartLocationData
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime LatestUpdate { get; set; }
    }
}
