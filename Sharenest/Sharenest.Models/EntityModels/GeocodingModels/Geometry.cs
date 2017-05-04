namespace Sharenest.Models.EntityModels.GeocodingModels
{
    public class Geometry
    {
        public GeometryLocation location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }
}
