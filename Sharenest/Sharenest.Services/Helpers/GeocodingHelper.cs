using System.Linq;
using Newtonsoft.Json;
using Sharenest.Models.EntityModels.GeocodingModels;

namespace Sharenest.Services.Helpers
{
    public static class GeocodingHelper
    {
        private static string appKey = "AIzaSyB5TC-EyM2QYJVd3s5rephYxfKjZ4FrFzA";
        private static string googlePlaceAPIUrl =
            "https://maps.googleapis.com/maps/api/geocode/json?address={0}+{1}&key={2}\r\n";

        public static void SetLocation(Sharenest.Models.EntityModels.Location location)
        {
            string placeApiUrl = string.Format(googlePlaceAPIUrl, location.LocationName, location.Country, appKey);
            var result = new System.Net.WebClient().DownloadString(placeApiUrl);
            var jsonobject = JsonConvert.DeserializeObject<RootObject>(result).results.First();

            location.Latitude = decimal.Parse(jsonobject.geometry.location.lat);
            location.Longitude = decimal.Parse(jsonobject.geometry.location.lng);
        }
    }
}
