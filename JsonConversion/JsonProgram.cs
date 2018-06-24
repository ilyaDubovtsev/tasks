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
			var v3 = new WarehoseConvertor().ConvertV2ToV3(json);
			Console.Write(v3);
		}
	}

	public class WarehoseConvertor
	{
		public string ConvertV2ToV3(string oldVersion)
		{
			var productWarehouseV2 = JsonConvert.DeserializeObject<ProductWarehouseV2>(oldVersion);
			return JsonConvert.SerializeObject(new ProductWarehouseV3
			{
				version = "3",
				products = productWarehouseV2
					.products
					.Select(x => new ProductV3(x.Value, x.Key))
					.ToArray()
			});
			
		}
	}

	public class ProductWarehouseV3
	{
		public string version { get; set; }
		public ProductV3[] products { get; set; }
	}

	public class ProductWarehouseV2
	{
		public int version { get; set; }
		public Dictionary<int, ProductV2> products { get; set; }
	}

	public class  ProductV2
	{
		public string name { get; set; }
		public double price { get; set; }
		public long count { get; set; }
	}

	public class ProductV3
	{
		public int id { get; set; }
		public string name { get; set; }
		public double price { get; set; }
		public long count { get; set; }

		public ProductV3(ProductV2 old, int id)
		{
			this.id = id;
			name = old.name;
			price = old.price;
			count = old.count;
		}

		private string DoublePrettify(double d)
		{
			if (Math.Abs(d - (int) d) < 0.000001)
				return $"{(int)d}";
			return d.ToString(System.Globalization.CultureInfo.InvariantCulture);
		}
	}


}
