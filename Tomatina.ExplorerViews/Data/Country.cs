using Newtonsoft.Json;

namespace Tomatina.ExplorerViews.Data
{
    
   [JsonObject(MemberSerialization.OptIn)]
    public class Country
    {
        [JsonProperty("country")]
        public string Code { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("lat")]
        public string Latitude { get; set; }


        [JsonProperty("lng")]
        public string Longitude { get; set; }
    }
}
