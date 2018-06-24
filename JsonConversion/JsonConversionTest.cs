using System.IO;
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
            var v2 = File.ReadAllText("..\\..\\1.v2.json");
            var expected = File.ReadAllText("..\\..\\1.v3.json").Replace(" ", "").Replace("\n", "").Replace("\t", "");

            var actual = new WarehoseConvertor().ConvertV2ToV3(v2);

            actual.Should().Be(expected);
        }
    }
}
