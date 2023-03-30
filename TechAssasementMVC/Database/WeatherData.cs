using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechAssasementMVC.Database
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public int Clouds { get; set; }
        public DateTime Time { get; set; }
    }
}
