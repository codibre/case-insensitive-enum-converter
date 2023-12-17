using System.Text.Json;
using Codibre.CaseInsensitiveEnum;
using Newtonsoft.Json.Serialization;

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

        class TestClass {
            public TestEnum Test { get; set; }
        }

        [TestMethod]
        public void Should_Deserialize_Enum_First_Value() {
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
        public void Should_Deserialize_Enum_Second_Value() {
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
        public void Should_Deserialize_Enum_Any_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
            };
            var converted = JsonSerializer.Deserialize<TestEnum>(@"""C""", options);

            converted.Should().Be(TestEnum.c);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_First_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var expectation = new
            {
                Test = TestEnum.a,
            };
            
            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_Second_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var expectation = new
            {
                Test = TestEnum.b,
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_Enum_Property_Any_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var expectation = new
            {
                Test = TestEnum.c,
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(expectation);
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_First_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var expectation = new
            {
                Test = TestEnum.a.ToString().ToUpper(),
            };
            
            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.a,
            });
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_Second_Value() {
            var options = new JsonSerializerOptions
            {
                Converters = { new CaseInsensitiveEnumConverter() },
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var expectation = new
            {
                Test = TestEnum.b.ToString().ToUpper(),
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.b,
            });
        }

        [TestMethod]
        public void Should_Deserialize_String_Enum_Property_Any_Value() {
            var test = new CaseInsensitiveEnumConverter();
            var options = new JsonSerializerOptions
            {
                Converters = { test },
                WriteIndented = true,
            };
            var expectation = new
            {
                Test = TestEnum.c.ToString().ToUpper(),
            };

            var converted = JsonSerializer.Deserialize<TestClass>(JsonSerializer.Serialize(expectation), options);

            converted.Should().BeEquivalentTo(new
            {
                Test = TestEnum.c,
            });
        }
    }
}
