using Newtonsoft.Json;

namespace JsonConversion
{
    public class ProductV2
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("count")]
        public long Count { get; set; }
    }
}