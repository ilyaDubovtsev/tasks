using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JsonConversion
{
	class JsonProgram
	{
		static void Main()
		{
			string json = Console.In.ReadToEnd();
			var v3 = new WarehoseConvertor().Convert(json);
			Console.Write(v3);
		}
	}

	public class WarehoseConvertor
	{
		public string Convert(string oldVersion)
		{
			JObject v2 = JObject.Parse(oldVersion);
			var productWarehouseV2 = JsonConvert.DeserializeObject<ProductWarehouseV2>(oldVersion);
			return JsonConvert.SerializeObject(new ProductWarehouse
			{
				Version = 3,
				Products = productWarehouseV2.Products.Select(x => new ProductV3(x.Value, x.Key)).ToArray()
			});

			//var products = v2.Property("products");
			//var productV2s = products.Value<Dictionary<int, ProductV2>>();
			//var productV3s = productV2s
			//	.Select(x => new ProductV3(x.Value, x.Key))
			//	.ToArray();
			//var productWarehouse = JsonConvert.SerializeObject(new ProductWarehouse { Products = productV3s });

			//return "{ 'version':'3', 'products': '" + productWarehouse + "' }";
		}
	}

	public class ProductWarehouse
	{
		public int Version { get; set; }
		public ProductV3[] Products { get; set; }
	}

	public class ProductWarehouseV2
	{
		public int Version { get; set; }
		public Dictionary<int, ProductV2> Products { get; set; }
	}

	public class ProductPair
	{
		public int Id { get; set; }
		public ProductV2 Product { get; set; }
	}

	public class  ProductV2
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Count { get; set; }
	}

	public class ProductV3
	{
		public int id { get; set; }
		public string name { get; set; }
		public decimal price { get; set; }
		public int count { get; set; }

		public ProductV3(ProductV2 old, int id)
		{
			this.id = id;
			name = old.Name;
			price = old.Price;
			count = old.Count;
		}
	}
}
