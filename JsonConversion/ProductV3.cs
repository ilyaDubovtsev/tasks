using Newtonsoft.Json;

namespace JsonConversion
{
	public class ProductV3
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("price")]
		public double Price { get; set; }
		[JsonProperty("count")]
		public long Count { get; set; }

		public ProductV3(ProductV2 old, int id)
		{
			Id = id;
			Name = old.Name;
			Price = old.Price;
			Count = old.Count;
		}
	}
}