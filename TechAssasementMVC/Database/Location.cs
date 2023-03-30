using System.ComponentModel.DataAnnotations;

namespace TechAssasementMVC.Database
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
