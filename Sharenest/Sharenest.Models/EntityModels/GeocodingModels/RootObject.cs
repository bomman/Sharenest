using System.Collections.Generic;

namespace Sharenest.Models.EntityModels.GeocodingModels
{
    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

}
