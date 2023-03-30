using System.Text.Json.Serialization;

namespace TechAssasementMVC.Dtos
{

    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("tz_id")]
        public string Tz_id { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public long Localtime_epoch { get; set; }

        [JsonPropertyName("localtime")]
        public string Localtime { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class Current
    {
        [JsonPropertyName("last_updated_epoch")]
        public long Last_updated_epoch { get; set; }

        [JsonPropertyName("last_updated")]
        public string Last_updated { get; set; }

        [JsonPropertyName("temp_c")]
        public double Temp_c { get; set; }

        [JsonPropertyName("temp_f")]
        public double Temp_f { get; set; }

        [JsonPropertyName("is_day")]
        public int Is_day { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

        [JsonPropertyName("wind_mph")]
        public double Wind_mph { get; set; }

        [JsonPropertyName("wind_kph")]
        public double Wind_kph { get; set; }

        [JsonPropertyName("wind_degree")]
        public int Wind_degree { get; set; }

        [JsonPropertyName("wind_dir")]
        public string Wind_dir { get; set; }

        [JsonPropertyName("pressure_mb")]
        public double Pressure_mb { get; set; }

        [JsonPropertyName("pressure_in")]
        public double Pressure_in { get; set; }

        [JsonPropertyName("precip_mm")]
        public double Precip_mm { get; set; }

        [JsonPropertyName("precip_in")]
        public double Precip_in { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }

        [JsonPropertyName("feelslike_c")]
        public double Feelslike_c { get; set; }

        [JsonPropertyName("feelslike_f")]
        public double Feelslike_f { get; set; }

        [JsonPropertyName("vis_km")]
        public double Vis_km { get; set; }

        [JsonPropertyName("vis_miles")]
        public double Vis_miles { get; set; }

        [JsonPropertyName("uv")]
        public double Uv { get; set; }

        [JsonPropertyName("gust_mph")]
        public double Gust_mph { get; set; }

        [JsonPropertyName("gust_kph")]
        public double Gust_kph { get; set; }
    }

    public class WeatherDataDto
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }
}
