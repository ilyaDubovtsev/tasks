using Newtonsoft.Json;

namespace JsonConversion
{
    public class ProductWarehouseV3
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("products")]
        public ProductV3[] Products { get; set; }
    }
}