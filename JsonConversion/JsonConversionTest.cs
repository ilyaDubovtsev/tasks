using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;


namespace JsonConversion
{
	[TestFixture]
	class JsonConversionTest
	{
		
		[Test]
		public void WarehouseConvertor_ShouldConvert()
		{
			var v2 = File.ReadAllText("D:\\Контур.Кампус\\tasks\\JsonConversion\\1.v2.json");
			var v3 = File.ReadAllText("D:\\Контур.Кампус\\tasks\\JsonConversion\\1.v3.json");

			var result = new WarehoseConvertor().Convert(v2);

			result.Should().Be(v3);
		}
	}
}
