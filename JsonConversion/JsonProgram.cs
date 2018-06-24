using System;
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
				Version = "3",
				Products = productWarehouseV2
					.Products
					.Select(x => new ProductV3(x.Value, x.Key))
					.ToArray()
			});
		}
	}
}
