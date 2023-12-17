using System.Text.Json;
using Codibre.CaseInsensitiveEnum;

namespace Test
{
    
    
    [TestClass]
    public class CaseInsensitiveEnumConverterTest
    {   
        enum TestEnum {
            a,
            b,
            c,
        }

        class DeseTest {
            public TestEnum Test { get; set; }
        }

        [TestMethod]
        public void EnumTest() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""B""", options);

            converted.Should().Be(TestEnum.b);
        }
        [TestMethod]
        public void EnumTest2() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""A""", options);

            converted.Should().Be(TestEnum.a);
        }
        [TestMethod]
        public void EnumTest3() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
            };
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""A""", options);

            converted.Should().Be(TestEnum.a);
        }
    }
}
